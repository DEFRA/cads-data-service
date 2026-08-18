using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Application.Imports.Utilities;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.BuildingBlocks.Core.DTOs;
using Cads.Cds.StorageBridge.Application.Imports.Repositories;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Metrics;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;
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

        await using var scope = serviceScopeFactory.CreateAsyncScope();

        var fileImportRepository = scope.ServiceProvider.GetRequiredService<IStorageBridgeFileImportRepository>();

        var fileImport = await fileImportRepository.GetByIdAsync(job.FileImportId, cancellationToken)
                ?? throw new InvalidOperationException($"FileImport with ID {job.FileImportId} not found.");

        var (importDataType, importActionType, schemaName) = GetImportParameters(fileImport.FileName);

        if (importDataType == ImportDataType.None)
        {
            throw new InvalidOperationException($"Failed to extract destination table from filename: {fileImport.FileName}");
        }

        var filePath = $"import/{Path.GetFileNameWithoutExtension(fileImport.FileName)}";

        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("Starting CSV import copy for job {JobId} with key {FilePath}",
                job.JobId, filePath);
        }

        _storageService = scope.ServiceProvider.GetRequiredService<IStorageService<CadsInternalClient>>();

        var keys = await _storageService.ListKeysAsync(filePath, cancellationToken);

        if (!keys.Any()) return 0;

        var dbContext = scope.ServiceProvider.GetRequiredService<StorageBridgeWriteDbContext>();
        var connection = await OpenConnectionAsync(dbContext, cancellationToken);

        var factoryProvider = scope.ServiceProvider.GetRequiredService<IS3ImportCommandFactoryProvider>();
        var factory = factoryProvider.Create((NpgsqlConnection)connection);
        var createTempTableCommand = factory.CreateTempTableCommand(importDataType, schemaName, importActionType, fileImport.Id);
        var actionCommands = await GetCommandsAsync(importDataType, schemaName, importActionType, factory, cancellationToken);

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
                schemaName,
                job.Delimiter,
                factory,
                dbContext,
                createTempTableCommand,
                actionCommands,
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
                job.JobId, fileImport.FileName, totalRows, sw.Elapsed.TotalMilliseconds);
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
    /// <param name="factory"></param>
    /// <param name="dbContext"></param>
    /// <param name="maxRetryAttempts"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ExcludeFromCodeCoverage]
    private async Task<int> ProcessFileAsync(
        string key,
        ImportDataType importDataType,
        SchemaName schemaName,
        char delimiter,
        IS3ImportCommandFactory factory,
        StorageBridgeWriteDbContext dbContext,
        DbCommand createTempTableCommand,
        List<DbCommand> actionCommands,
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

                    if (!IsTransient(ex))
                    {
                        logger.LogError(ex, "Operation {Operation} for key {Key} failed with a non-transient error on attempt {Attempt}; not retrying",
                            operationName, key, attempt);
                        throw;
                    }

                    if (attempt >= maxAttempts)
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
            var connection = (NpgsqlConnection)await OpenConnectionAsync(dbContext, cancellationToken);

            // Begin transaction and ensure proper rollback on error
            await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

            try
            {
                // Create temp table (with retries for transient DB issues)
                createTempTableCommand.Connection = connection;
                createTempTableCommand.Transaction = transaction;

                await createTempTableCommand.ExecuteNonQueryAsync(cancellationToken);

                // Copy file to staging (may involve network IO; add retry)
                await CopyFileToStagingAsync(importDataType, schemaName, delimiter, key, factory, cancellationToken);

                // Execute action commands (retry the whole command set if transient)
                foreach (var command in actionCommands)
                {
                    command.Connection = connection;
                    command.Transaction = transaction;
                }

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
                    logger.LogInformation(ex, "NpgsqlException details: {Message}, SqlState: {SqlState}, ErrorCode: {ErrorCode}, ConnectionState: {connectionState}",
                        ex.Message, ex.SqlState, ex.ErrorCode, connection.State.ToString());
                }

                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process file {Key}", key);

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

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("Command: {CommandText}", command.CommandText);
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

    [ExcludeFromCodeCoverage]
    private static async Task<DbConnection> OpenConnectionAsync(
        StorageBridgeWriteDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var connection = dbContext.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        return connection;
    }

    private static async Task<List<DbCommand>> GetCommandsAsync(
        ImportDataType importDataType,
        SchemaName schemaName,
        ImportActionType importActionType,
        IS3ImportCommandFactory factory,
        CancellationToken cancellationToken)
    {
        var commands = new List<DbCommand>();

        switch (importActionType)
        {
            // Both Bulk and Delta currently insert into cts-transactions the same way
            case ImportActionType.Bulk:
            case ImportActionType.Delta:
                commands.Add(await factory.CreateInsertCommandAsync(importDataType, schemaName, cancellationToken));
                break;
            default:
                throw new InvalidOperationException($"Unsupported ImportActionType '{importActionType}'.");
        }

        return commands;
    }

    private static (ImportDataType ImportDataType, ImportActionType ImportActionType, SchemaName SchemaName) GetImportParameters(string filename)
    {
        var parsedFilename = CtsmFilenameParser.Parse(filename);

        if (!Enum.TryParse<ImportActionType>(parsedFilename?.Type, true, out var importActionType))
        {
            throw new InvalidOperationException($"Invalid ImportActionType '{parsedFilename?.Type}' for file '{filename}'.");
        }

        var schemaName = importActionType.GetSchemaName();

        var importDataType = Enum.GetValues<ImportDataType>()
            .FirstOrDefault(v => v.GetTableName(schemaName)?.Equals(parsedFilename?.TableName, StringComparison.InvariantCultureIgnoreCase) == true);

        return (importDataType, importActionType, schemaName);
    }

    // Postgres SQLSTATE classes worth retrying — connection drops, deadlocks/serialization
    // conflicts, resource exhaustion, and "try again shortly" conditions. Anything else
    // (bad data, constraint violations, bad SQL, permissions) will fail identically on
    // every attempt, so it defaults to non-transient rather than being retried blindly.
    private static readonly HashSet<string> s_transientPostgresSqlStateClasses = new(StringComparer.Ordinal)
    {
        "08", // Connection Exception
        "40", // Transaction Rollback — serialization_failure, deadlock_detected
        "53", // Insufficient Resources — too_many_connections, disk_full, out_of_memory
        "57", // Operator Intervention — e.g. 57P03 cannot_connect_now (server still starting up)
        "58", // System Error — I/O failures
    };

    /// <summary>
    /// Whether an exception represents a transient failure worth retrying, versus a permanent
    /// one (bad data, malformed SQL, constraint violation) that will fail identically on every
    /// attempt and should fail fast instead of wasting backoff time.
    /// </summary>
    private static bool IsTransient(Exception ex) => ex switch
    {
        PostgresException pgEx => IsTransientPostgresSqlState(pgEx.SqlState),
        NpgsqlException => true,   // client/connection-level Npgsql errors not wrapping a specific Postgres error
        DbException => true,
        IOException => true,
        TimeoutException => true,
        _ => false
    };

    private static bool IsTransientPostgresSqlState(string? sqlState) =>
        !string.IsNullOrEmpty(sqlState)
        && sqlState.Length >= 2
        && s_transientPostgresSqlStateClasses.Contains(sqlState[..2]);
}