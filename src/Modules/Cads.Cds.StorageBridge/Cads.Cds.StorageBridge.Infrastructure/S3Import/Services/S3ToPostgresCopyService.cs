using Cads.Cds.BuildingBlocks.Application.Imports;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.StorageBridge.Application.Extensions;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Metrics;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Helpers;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;

public class S3ToPostgresCopyService(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<S3ToPostgresCopyService> logger) : IS3ToPostgresCopyService
{
    private IStorageService<CadsInternalClient> _storageService = null!;

    /// <summary>
    /// Cannot utilise low-level PostgreSQL/Persistence types using In Memory DB.
    /// </summary>
    /// <param name="job"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ExcludeFromCodeCoverage]
    public async Task<int> ExecuteAsync(CreateS3CsvImportJobDto job, CancellationToken cancellationToken = default)
    {
        const int MaxRetryAttempts = 3;

        ValidateJob(job);

        if (!S3Utils.TryParseS3Url(job.SourceKey, out var _, out var _, out var fileName))
        {
            logger.LogError("Failed to parse S3 URL: {SourceKey}", job.SourceKey);
            throw new InvalidOperationException("Failed to parse S3 URL");
        }

        if (string.IsNullOrWhiteSpace(fileName))
        {
            logger.LogError("Failed to extract file name from S3 URL: {SourceKey}", job.SourceKey);
            throw new InvalidOperationException("Failed to extract file name from S3 URL");
        }

        var (importDataType, importActionType) = GetImportParameters(fileName);

        if (importDataType == ImportDataType.None)
        {
            logger.LogError("Failed to extract destination table from S3 URL: {SourceKey}", job.SourceKey);
            throw new InvalidOperationException("Failed to extract destination table from S3 URL");
        }

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Starting CSV import copy for job {JobId} with key SourceKey {SourceKey}",
                job.JobId, job.SourceKey);
        }

        await using var scope = serviceScopeFactory.CreateAsyncScope();

        _storageService = scope.ServiceProvider.GetRequiredService<IStorageService<CadsInternalClient>>();

        var keys = await _storageService.ListKeysAsync(job.SourceKey, cancellationToken);
        if (!keys.Any()) return 0;

        var dbContext = scope.ServiceProvider.GetRequiredService<StorageBridgeWriteDbContext>();
        var factoryProvider = scope.ServiceProvider.GetRequiredService<IS3ImportCommandFactoryProvider>();

        var (counter, fileHistogram, batchHistogram) = S3ImportMetrics.CreateBulkLoadMetrics();

        var sw = Stopwatch.StartNew();
        var totalRows = 0;

        foreach (var key in keys)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Processing file {Key} for CSV import job {JobId}", key, job.JobId);
            }

            var fileSw = Stopwatch.StartNew();

            var rows = await ProcessFileAsync(
                key,
                importDataType,
                importActionType,
                job.Delimiter,
                factoryProvider,
                dbContext,
                MaxRetryAttempts,
                cancellationToken);

            totalRows += rows;
            counter.Add(rows);

            fileHistogram.Record(fileSw.Elapsed.TotalMilliseconds);

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Completed processing for file {Key} for CSV import job {JobId}, {TotalRows} records processed in {TotalMilliseconds} ms",
                    key, job.JobId, rows, fileSw.Elapsed.TotalMilliseconds);
            }
        }

        batchHistogram.Record(sw.Elapsed.TotalMilliseconds);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Completed CSV import copy for job {JobId} with key {SourceKey}, {TotalRows} records processed in {TotalMilliseconds} ms",
                job.JobId, job.SourceKey, totalRows, sw.Elapsed.TotalMilliseconds);
        }

        return totalRows;
    }

    /// <summary>
    /// Cannot utilise low-level PostgreSQL/Persistence types using In Memory DB.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="importDataType"></param>
    /// <param name="importActionType"></param>
    /// <param name="delimiter"></param>
    /// <param name="factoryProvider"></param>
    /// <param name="dbContext"></param>
    /// <param name="maxRetryAttempts"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ExcludeFromCodeCoverage]
    private async Task<int> ProcessFileAsync(
        string key,
        ImportDataType importDataType,
        ImportActionType importActionType,
        char delimiter,
        IS3ImportCommandFactoryProvider factoryProvider,
        StorageBridgeWriteDbContext dbContext,
        int maxRetryAttempts = 3,
        CancellationToken cancellationToken = default)
    {
        static TimeSpan BackoffDelay(int attempt) => TimeSpan.FromSeconds(Math.Pow(2, attempt)); // 2s, 4s, 8s

        // Generic retry helper for transient failures
        async Task<T> RetryAsync<T>(Func<Task<T>> operation, string operationName, int maxAttempts)
        {
            var attempt = 0;

            while (true)
            {
                try
                {
                    return await operation();
                }
                catch (OperationCanceledException)
                {
                    // Cancellation should propagate immediately
                    throw;
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    attempt++;

                    // Consider DbException, IO and timeout-like exceptions as transient
                    var isTransient = ex is DbException
                                  || ex is IOException
                                  || ex is TimeoutException
                                  || ex is NpgsqlException;

                    if (attempt >= maxAttempts || !isTransient)
                    {
                        logger.LogError(ex, "Operation {Operation} for key {Key} failed permanently after {Attempt} attempts", operationName, key, attempt);
                        throw;
                    }

                    var delay = BackoffDelay(attempt);
                    logger.LogWarning(ex, "Transient failure on operation {Operation} for key {Key}. Retrying {Attempt}/{MaxAttempts} after {Delay}ms",
                        operationName, key, attempt, maxAttempts, delay.TotalMilliseconds);

                    try
                    {
                        await Task.Delay(delay, cancellationToken);
                    }
                    catch (OperationCanceledException) { throw; }
                }
            }
        }

        var rows = await RetryAsync(async () =>
        {
            var connection = (NpgsqlConnection)await GetConnectionAsync(dbContext, cancellationToken);

            // Begin transaction and ensure proper rollback on error
            await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

            try
            {
                var factory = factoryProvider.Create(connection, transaction);
                var createTempTableCommand = factory.CreateTempTableCommand(importDataType, importActionType.GetSchemaName());
                var actionCommands = await GetCommandsAsync(importDataType, importActionType, factory, cancellationToken);

                // Create temp table (with retries for transient DB issues)
                await createTempTableCommand.ExecuteNonQueryAsync(cancellationToken);

                // Copy file to staging (may involve network IO; add retry)
                await CopyFileToStagingAsync(importDataType, importActionType.GetSchemaName(), delimiter, key, factory, cancellationToken);

                // Execute action commands (retry the whole command set if transient)
                var rows = await ExecuteActionCommandsAsync(actionCommands, cancellationToken);

                // Commit once everything succeeds
                await transaction.CommitAsync(cancellationToken);

                return rows;
            }
            catch (OperationCanceledException)
            {
                try
                {
                    await transaction.RollbackAsync(CancellationToken.None);
                }
                catch (Exception rbEx)
                {
                    logger.LogWarning(rbEx, "Rollback after cancellation failed for key {Key}", key);
                }

                throw;
            }
            catch (NpgsqlException ex)
            {
                // Attempt rollback, but do not swallow original exception
                try
                {
                    await transaction.RollbackAsync(CancellationToken.None);
                }
                catch (Exception rbEx)
                {
                    // avoid evaluating rbEx.Message unnecessarily; exception is logged already
                    logger.LogError(rbEx, "Rollback failed for key {Key}", key);
                }

                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("NpgsqlException details: {Message}, SQLState: {SQLState}, ErrorCode: {ErrorCode}, ConnectionState: {connectionState}",
                        ex.Message, ex.SqlState, ex.ErrorCode, connection.State.ToString());
                }

                throw;
            }
            catch (Exception ex)
            {
                // Attempt rollback, but do not swallow original exception
                try
                {
                    await transaction.RollbackAsync(CancellationToken.None);
                }
                catch (Exception rbEx)
                {
                    // avoid evaluating rbEx.Message unnecessarily; exception is logged already
                    logger.LogError(rbEx, "Rollback failed for key {Key}", key);
                }

                logger.LogError(ex, "Failed to process file {Key}", key);
                throw;
            }

        }, nameof(ProcessFileAsync), maxRetryAttempts);

        return rows;
    }

    private async Task<int> ExecuteActionCommandsAsync(
        IEnumerable<DbCommand> actionCommands,
        CancellationToken cancellationToken)
    {
        var total = 0;

        foreach (var command in actionCommands)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (command is null) continue;

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Command: {CommandText}", command.CommandText);
            }

            total += await command.ExecuteNonQueryAsync(cancellationToken);
        }

        return total;
    }

    private async Task CopyFileToStagingAsync(
        ImportDataType bulkLoadDataType,
        SchemaName schemaName,
        char delimiter,
        string key,
        IS3ImportCommandFactory factory,
        CancellationToken cancellationToken)
    {
        using var response = await _storageService.GetObjectResponseAsync(key, cancellationToken);

        if (response?.ResponseStream is null)
        {
            logger.LogWarning("Null stream for key {Key}", key);
            return;
        }

        using var reader = new StreamReader(response.ResponseStream);

        var header = await reader.ReadLineAsync(cancellationToken)
            ?? throw new InvalidOperationException($"File {key} is empty or missing header row.");

        var fileColumns = header.Split(delimiter);

        if (!string.Equals(fileColumns[0], "record_type", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                $"File {key} does not contain a valid header row.");
        }

        var matchedColumns = await factory.FilterColumnsToTableAsync(
            bulkLoadDataType,
            schemaName,
            fileColumns,
            cancellationToken);

        using var writer = factory.CreateTextImport(bulkLoadDataType, schemaName, delimiter, matchedColumns);

        string? line;
        while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (line.StartsWith("T|")) break;
            await writer.WriteLineAsync(SanitiseLine(line));
        }
    }

    private static string? SanitiseLine(string? line)
    {
        var sanitisedResult = line ?? string.Empty;

        sanitisedResult = sanitisedResult.Replace("\"", "\"\"");

        sanitisedResult = Regex.Replace(
            sanitisedResult,
            @"[\u0000-\u001F]",
            " ",
            RegexOptions.None,
            TimeSpan.FromMilliseconds(50));

        return sanitisedResult;
    }

    private static void ValidateJob(CreateS3CsvImportJobDto job)
    {
        if (string.IsNullOrWhiteSpace(job.SourceKey))
            throw new InvalidOperationException("SourceKey is required.");
    }

    [ExcludeFromCodeCoverage]
    private static async Task<DbConnection> GetConnectionAsync(
        StorageBridgeWriteDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var connection = dbContext.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        return connection;
    }

    private TDbContext CreateDbContext<TDbContext>()
    where TDbContext : DbContext
    {
        using var scope = serviceScopeFactory.CreateScope();

        var dataSourceFactory = scope.ServiceProvider.GetRequiredService<IPostgresDataSourceFactory>();
        var dataSource = dataSourceFactory.CreateDataSource(PostgresDataSourceFactory.DefaultConnectionIdentifier);

        var options = new DbContextOptionsBuilder<TDbContext>()
            .UseNpgsql(dataSource.ConnectionString)
            .Options;

        return Activator.CreateInstance(typeof(TDbContext), options) as TDbContext
            ?? throw new InvalidOperationException($"Unable to create {typeof(TDbContext).Name}");
    }

    private static async Task<List<DbCommand>> GetCommandsAsync(
        ImportDataType importDataType,
        ImportActionType importActionType,
        IS3ImportCommandFactory factory,
        CancellationToken cancellationToken)
    {
        var commands = new List<DbCommand>();
        var schemaName = importActionType.GetSchemaName();

        switch (importActionType)
        {
            // Both Bulk and Delta will be inserting into cts-transactions
            case ImportActionType.Bulk:

            case ImportActionType.Delta:
                commands.Add(await factory.CreateInsertCommandAsync(importDataType, schemaName, cancellationToken));
                break;
            default:
                throw new InvalidOperationException($"Unsupported ImportActionType '{importActionType}'.");
        }

        return commands;
    }

    private static (ImportDataType, ImportActionType) GetImportParameters(string filename)
    {
        var parsedFilename = CtsmFilenameParser.Parse(filename);

        if (!Enum.TryParse<ImportActionType>(parsedFilename?.Type, true, out var importActionTypeParsed))
        {
            throw new InvalidOperationException($"Invalid ImportActionType '{parsedFilename?.Type}' for file '{filename}'.");
        }

        var schemaName = importActionTypeParsed.GetSchemaName();

        var importDataType = Enum.GetValues<ImportDataType>()
            .FirstOrDefault(v => v.GetTableName(schemaName)?.Equals(parsedFilename?.TableName, StringComparison.InvariantCultureIgnoreCase) == true);

        return (importDataType, importActionTypeParsed);
    }
}