using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.DTOs;
using Cads.Cds.StorageBridge.Application.Imports.Repositories;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Metrics;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Extensions;
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
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Models;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;

public class S3ToPostgresCopyService(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<S3ToPostgresCopyService> logger) : IS3ToPostgresCopyService
{
    private IStorageService<CadsInternalClient> _storageService = null!;
    private const int MaxRetryAttempts = 3;

    /// <summary>
    /// Cannot utilise low-level PostgreSQL/Persistence types using In Memory DB.
    /// </summary>
    /// <param name="job"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ExcludeFromCodeCoverage]
    public async Task<long> ExecuteAsync(CreateS3CsvImportJobDto job, CancellationToken cancellationToken = default)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();

        var fileImport = await GetFileImportAsync(job, scope, cancellationToken);

        var keys = await GetKeysFromStorage(fileImport, scope, cancellationToken);

        if (keys.Count == 0) return 0;

        var importContext = await ImportExecutionContext.CreateAsync(
            fileImport,
            job.Delimiter,
            scope.ServiceProvider,
            cancellationToken);

        var (counter, fileHistogram, batchHistogram) = S3ImportMetrics.CreateBulkLoadMetrics();

        var sw = Stopwatch.StartNew();

        var totalRowsImported = fileImport.RowsImported;

        foreach (var key in keys)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Processing file {Key} for CSV import job {JobId}", key, job.JobId);
            }

            var fileSw = Stopwatch.StartNew();

            var fileContext = new FileExecutionContext(importContext, key);
            var rows = await ProcessFileAsync(fileContext, cancellationToken);

            totalRowsImported += rows;
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
                job.JobId, fileImport.FileName, totalRowsImported, sw.Elapsed.TotalMilliseconds);
        }

        return totalRowsImported;
    }

    private async Task<FileImport> GetFileImportAsync(CreateS3CsvImportJobDto job, AsyncServiceScope scope, CancellationToken cancellationToken)
    {
        var fileImportRepository = scope.ServiceProvider.GetRequiredService<IStorageBridgeFileImportRepository>();
        var fileImport = await fileImportRepository.GetByIdAsync(job.FileImportId, cancellationToken)
                ?? throw new InvalidOperationException($"FileImport with ID {job.FileImportId} not found.");
        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("Starting CSV import copy for job {JobId} with key {FilePath}",
                job.JobId, fileImport.FileName);
        }
        return fileImport;
    }

    private async Task<List<string>> GetKeysFromStorage(FileImport fileImport, AsyncServiceScope scope, CancellationToken cancellationToken)
    {
        var filePath = GetSplitPartsPrefix(fileImport);
        _storageService = scope.ServiceProvider.GetRequiredService<IStorageService<CadsInternalClient>>();

        var keys = await _storageService.ListKeysAsync(filePath, fileImport.LastFilePartImported, cancellationToken);
        return [.. keys];
    }

    /// <summary>
    /// Cannot utilise low-level PostgreSQL/Persistence types using In Memory DB.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ExcludeFromCodeCoverage]
    private Task<int> ProcessFileAsync(
        FileExecutionContext context,
        CancellationToken cancellationToken = default)
    {
        return ExecuteWithTransientRetryAsync(
            operation: (useDefensiveCopyMode, ct) => ProcessFileOnceAsync(context, useDefensiveCopyMode, ct),
            operationName: nameof(ProcessFileAsync),
            key: context.Key,
            cancellationToken: cancellationToken);
    }

    [ExcludeFromCodeCoverage]
    private async Task<int> ProcessFileOnceAsync(
        FileExecutionContext fileExecutionContext,
        bool useDefensiveCopyMode = false,
        CancellationToken cancellationToken = default)
    {
        var importExecutionContext = fileExecutionContext.ImportContext;
        var key = fileExecutionContext.Key;

        var connection = (NpgsqlConnection)await OpenConnectionAsync(importExecutionContext.DbContext, cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            importExecutionContext.CreateTempTableCommand.Connection = connection;
            importExecutionContext.CreateTempTableCommand.Transaction = transaction;
            await importExecutionContext.CreateTempTableCommand.ExecuteNonQueryAsync(cancellationToken);

            await CopyFileToStagingAsync(fileExecutionContext, useDefensiveCopyMode, cancellationToken);

            foreach (var command in importExecutionContext.ActionCommands)
            {
                command.Connection = connection;
                command.Transaction = transaction;
            }

            var rows = await ExecuteActionCommandsAsync(importExecutionContext.ActionCommands, cancellationToken);

            importExecutionContext.FileImport.LastFilePartImported = key;
            importExecutionContext.FileImport.RowsImported += rows;

            await importExecutionContext.DbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return rows;
        }
        catch (OperationCanceledException)
        {
            await RollbackWithLoggingAsync(transaction, key, LogLevel.Warning, "Rollback after cancellation failed for key {Key}");
            throw;
        }
        catch (NpgsqlException ex)
        {
            await RollbackWithLoggingAsync(transaction, key, LogLevel.Error, "Rollback failed for key {Key}");
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(ex, "An NpgsqlException has occurred. Current ConnectionState: {ConnectionState}",
                    connection.State.ToString());
            }
            throw;
        }
        catch (Exception ex)
        {
            await RollbackWithLoggingAsync(transaction, key, LogLevel.Error, "Rollback failed for key {Key}");
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Failed to process file {Key}", key);
            }
            throw;
        }
    }

    private async Task RollbackWithLoggingAsync(
        NpgsqlTransaction transaction,
        string key,
        LogLevel rollbackFailureLevel,
        string rollbackFailureMessageTemplate)
    {
        try
        {
            await transaction.RollbackAsync(CancellationToken.None);
        }
        catch (Exception rbEx)
        {
            if (logger.IsEnabled(rollbackFailureLevel))
            {
                logger.Log(rollbackFailureLevel, rbEx, rollbackFailureMessageTemplate, key);
            }
        }
    }

    private async Task<T> ExecuteWithTransientRetryAsync<T>(
        Func<bool, CancellationToken, Task<T>> operation,
        string operationName,
        string key,
        CancellationToken cancellationToken)
    {
        static TimeSpan BackoffDelay(int attempt) => TimeSpan.FromSeconds(Math.Pow(2, attempt)); // 2s, 4s, 8s

        var attempt = 0;
        var useDefensiveCopyMode = false;

        while (true)
        {
            try
            {
                var result = await operation(useDefensiveCopyMode, cancellationToken);
                useDefensiveCopyMode = false;
                return result;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                attempt++;
                // Check for COPY format issue (SQLSTATE 22P04) and retry with defensive mode if not already used
                if (ex is NpgsqlException npg && npg.SqlState == "22P04")
                {
                    if (!useDefensiveCopyMode)
                    {
                        useDefensiveCopyMode = true;
                        if (logger.IsEnabled(LogLevel.Warning))
                        {
                            logger.LogWarning(ex, "COPY format issue for key {Key}; retrying once with defensive mode", key);
                        }
                        continue;
                    }

                    if (logger.IsEnabled(LogLevel.Error))
                    {
                        logger.LogError(ex, "COPY format issue persisted for key {Key} after defensive retry", key);
                    }
                    throw;
                }
                // Check for transient Postgres exceptions and retry if applicable
                if (!ex.IsTransientPostgresException())
                {
                    if (logger.IsEnabled(LogLevel.Error))
                    {
                        logger.LogError(ex, "Operation {Operation} for key {Key} failed with a non-transient error on attempt {Attempt}; not retrying",
                            operationName, key, attempt);
                    }
                    throw;
                }
                // If we've reached the maximum number of retry attempts, log and rethrow
                if (attempt >= MaxRetryAttempts)
                {
                    if (logger.IsEnabled(LogLevel.Error))
                    {
                        logger.LogError(ex,
                            "Operation {Operation} for key {Key} failed permanently after {Attempt} attempts",
                            operationName, key, attempt);
                    }
                    throw;
                }
                // Log the transient failure and wait before retrying
                var delay = BackoffDelay(attempt);
                if (logger.IsEnabled(LogLevel.Warning))
                {
                    logger.LogWarning(ex, "Transient failure on operation {Operation} for key {Key}. Retrying {Attempt}/{MaxAttempts} after {Delay}ms",
                        operationName, key, attempt, MaxRetryAttempts, delay.TotalMilliseconds);
                }
                await Task.Delay(delay, cancellationToken);
            }
        }
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
        FileExecutionContext fileExecutionContext,
        bool useDefensiveCopyMode,
        CancellationToken cancellationToken)
    {
        var importExecutionContext = fileExecutionContext.ImportContext;
        var key = fileExecutionContext.Key;

        using var response = await _storageService.GetObjectResponseAsync(key, cancellationToken);

        if (response?.ResponseStream is null)
        {
            logger.LogWarning("Null stream for key {Key}", key);
            return;
        }

        using var reader = new StreamReader(response.ResponseStream);

        var header = await reader.ReadLineAsync(cancellationToken)
                     ?? throw new InvalidOperationException($"File {key} is empty or missing header row.");

        var fileColumns = header.Split(importExecutionContext.Delimiter);
        var fileColumnCount = fileColumns.Length;
        if (!string.Equals(fileColumns[0], "record_type", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"File {key} does not contain a valid header row.");
        }

        var matchedColumns = await importExecutionContext.Factory.FilterColumnsToTableAsync(
            importExecutionContext.ImportParameters.ImportDataType,
            importExecutionContext.ImportParameters.SchemaName,
            fileColumns,
            cancellationToken);

        using var writer = importExecutionContext.Factory.CreateTextImport(
            importExecutionContext.ImportParameters.ImportDataType,
            importExecutionContext.ImportParameters.SchemaName,
            importExecutionContext.Delimiter,
            matchedColumns);

        string? line;
        while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (line.StartsWith("T|")) break;
            line = SanitiseLine(line);
            if (useDefensiveCopyMode)
            {
                line = importExecutionContext.DefensiveCopyLineNormaliser.Normalise(
                    line!,
                    importExecutionContext.ImportParameters.ImportDataType,
                    importExecutionContext.Delimiter,
                    fileColumnCount);
            }
            await writer.WriteLineAsync(line);
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

    public static string GetSplitPartsPrefix(FileImport fileImport)
    {
        if (string.IsNullOrWhiteSpace(fileImport.DestinationPrefix))
        {
            throw new InvalidOperationException($"FileImport {fileImport.Id} ('{fileImport.FileName}') has no destination prefix.");
        }

        return $"{fileImport.DestinationPrefix.Trim('/')}/{Path.GetFileNameWithoutExtension(fileImport.FileName)}";
    }
}