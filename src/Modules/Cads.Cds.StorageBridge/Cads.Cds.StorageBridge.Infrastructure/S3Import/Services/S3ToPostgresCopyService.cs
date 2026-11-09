using Cads.Cds.BuildingBlocks.Application.Encryption;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Core.DTOs;
using Cads.Cds.BuildingBlocks.Infrastructure.Encryption.Configuration;
using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.StorageBridge.Application.Imports.Repositories;
using Cads.Cds.StorageBridge.Application.S3Import.Model;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Metrics;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Extensions;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Models;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;

public class S3ToPostgresCopyService(
    IServiceScopeFactory serviceScopeFactory,
    IFileEncryptor fileEncryptor,
    IOptions<EncryptionConfig> encryptionOptions,
    ILogger<S3ToPostgresCopyService> logger) : IS3ToPostgresCopyService
{
    private IStorageService<CadsInternalClient> _storageService = null!;
    private const int MaxRetryAttempts = 3;

    private sealed record ProcessFilesResult(int TotalRowsImported, List<string> AmendedRowIds);

    /// <summary>
    /// Cannot utilise low-level PostgreSQL/Persistence types using In Memory DB.
    /// </summary>
    /// <param name="job"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ExcludeFromCodeCoverage]
    public async Task<S3ToPostgresResult> ExecuteAsync(CreateS3CsvImportJobDto job,
        CancellationToken cancellationToken = default)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();

        var fileImport = await GetFileImportAsync(job, scope, cancellationToken);

        var keys = await GetKeysFromStorage(fileImport, scope, cancellationToken);

        if (keys.Count == 0) return new S3ToPostgresResult { TotalRowsProcessed = 0 };

        var dbContext = scope.ServiceProvider.GetRequiredService<StorageBridgeWriteDbContext>();
        var connection = (NpgsqlConnection)await OpenConnectionAsync(dbContext, cancellationToken);

        var factoryProvider = scope.ServiceProvider.GetRequiredService<IS3ImportCommandFactoryProvider>();
        var factory = factoryProvider.Create(connection);

        var defensiveCopyLineNormaliser = scope.ServiceProvider.GetRequiredService<IDefensiveCopyLineNormaliserService>();

        var importContext = await ImportExecutionContext.CreateAsync(
            fileImport,
            dbContext,
            job.Delimiter,
            factory,
            defensiveCopyLineNormaliser,
            cancellationToken);

        var (counter, fileHistogram, batchHistogram) = S3ImportMetrics.CreateBulkLoadMetrics();

        var sw = Stopwatch.StartNew();

        var totalRowsImported = fileImport.RowsImported;
        var amendedRowIds = new List<string>();

        foreach (var key in keys)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Processing file {Key} for CSV import job {JobId}", key, job.JobId);
            }

            var fileSw = Stopwatch.StartNew();

            var fileContext = new FileExecutionContext(importContext, key);
            var result = await ProcessFileAsync(fileContext, cancellationToken);

            totalRowsImported += result.TotalRowsImported;
            counter.Add(result.TotalRowsImported);
            amendedRowIds.AddRange(result.AmendedRowIds);

            fileHistogram.Record(fileSw.Elapsed.TotalMilliseconds);

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Completed processing for file {Key} for CSV import job {JobId}, {TotalRows} records processed in {TotalMilliseconds} ms",
                    key, job.JobId, result.TotalRowsImported, fileSw.Elapsed.TotalMilliseconds);
            }
        }

        batchHistogram.Record(sw.Elapsed.TotalMilliseconds);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Completed CSV import copy for job {JobId} with key {SourceKey}, {TotalRows} records processed in {TotalMilliseconds} ms",
                job.JobId, fileImport.FileName, totalRowsImported, sw.Elapsed.TotalMilliseconds);
        }

        return new S3ToPostgresResult
        {
            TotalRowsProcessed = totalRowsImported,
            RowIdsAmended = amendedRowIds
        };
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
    private Task<ProcessFilesResult> ProcessFileAsync(
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
    private async Task<ProcessFilesResult> ProcessFileOnceAsync(
        FileExecutionContext fileExecutionContext,
        bool useDefensiveCopyMode = false,
        CancellationToken cancellationToken = default)
    {
        var importExecutionContext = fileExecutionContext.ImportContext;
        var key = fileExecutionContext.Key;

        // Download and (if storage encryption is enabled) decrypt BEFORE taking the connection/transaction, so slow
        // S3 I/O and decryption don't hold them open. When decrypting, the file is fully authenticated by the time we
        // get a stream back, so nothing from a corrupt or tampered file ever reaches Postgres.
        using var fileObjectResponse = await _storageService.GetObjectResponseAsync(key, cancellationToken);
        await using var fileObjectSource = await OpenSourceAsync(fileObjectResponse?.ResponseStream, key, cancellationToken);

        var connection = (NpgsqlConnection)await OpenConnectionAsync(importExecutionContext.DbContext, cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            importExecutionContext.CreateTempTableCommand.Connection = connection;
            importExecutionContext.CreateTempTableCommand.Transaction = transaction;
            await importExecutionContext.CreateTempTableCommand.ExecuteNonQueryAsync(cancellationToken);

            var amendedRowIds = await CopyFileToStagingAsync(fileExecutionContext, fileObjectSource, useDefensiveCopyMode, cancellationToken);

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

            return new ProcessFilesResult(rows, amendedRowIds);
        }
        catch (OperationCanceledException)
        {
            await RollbackWithLoggingAsync(transaction, key, afterCancellation: true);
            throw;
        }
        catch (NpgsqlException ex)
        {
            await RollbackWithLoggingAsync(transaction, key, afterCancellation: false);
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(ex, "An NpgsqlException has occurred. Current ConnectionState: {ConnectionState}",
                    connection.State.ToString());
            }
            throw;
        }
        catch (Exception ex)
        {
            await RollbackWithLoggingAsync(transaction, key, afterCancellation: false);
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Failed to process file {Key}", key);
            }
            throw;
        }
    }

    [ExcludeFromCodeCoverage]
    private async Task RollbackWithLoggingAsync(
        NpgsqlTransaction transaction,
        string key,
        bool afterCancellation)
    {
        try
        {
            await transaction.RollbackAsync(CancellationToken.None);
        }
        catch (Exception rbEx)
        {
            if (afterCancellation)
            {
                if (logger.IsEnabled(LogLevel.Warning))
                {
                    logger.LogWarning(rbEx, "Rollback after cancellation failed for key {Key}", key);
                }
            }
            else if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(rbEx, "Rollback failed for key {Key}", key);
            }
        }
    }

    [ExcludeFromCodeCoverage]
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

    /// <summary>
    /// Returns the stream to import from. If storage encryption is disabled this is the downloaded stream as-is.
    /// Otherwise the download is decrypted to a temporary file and a read stream over the plaintext is returned; it
    /// deletes the file when disposed. Returns null if the object has no content stream.
    /// </summary>
    [ExcludeFromCodeCoverage]
    private async Task<Stream?> OpenSourceAsync(Stream? downloaded, string key, CancellationToken cancellationToken)
    {
        if (downloaded is null)
        {
            logger.LogWarning("Null stream for key {Key}", key);
            return null;
        }

        if (!encryptionOptions.Value.StorageEncryptionEnabled)
        {
            return downloaded;
        }

        var decryptedPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.csv");

        // Writes to a sibling temp file and moves into place only once the whole file has authenticated.
        await fileEncryptor.DecryptToFileAsync(downloaded, decryptedPath, cancellationToken: cancellationToken);

        try
        {
            return new FileStream(decryptedPath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 81920,
                FileOptions.Asynchronous | FileOptions.SequentialScan | FileOptions.DeleteOnClose);
        }
        catch
        {
            TryDeleteTempFile(decryptedPath);
            throw;
        }
    }

    private void TryDeleteTempFile(string path)
    {
        try
        {
            File.Delete(path);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            logger.LogWarning(ex, "Could not delete temporary file {Path}", path);
        }
    }

    private async Task<List<string>> CopyFileToStagingAsync(
        FileExecutionContext fileExecutionContext,
        Stream? fileObjectSource,
        bool useDefensiveCopyMode,
        CancellationToken cancellationToken)
    {
        var importExecutionContext = fileExecutionContext.ImportContext;
        var key = fileExecutionContext.Key;
        var amendedRowIds = new List<string>();

        // No content stream (already logged in OpenSourceAsync): same behaviour as before, nothing staged.
        if (fileObjectSource is null) return amendedRowIds;

        using var reader = new StreamReader(fileObjectSource);

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
                var nomalisedLine = importExecutionContext.DefensiveCopyLineNormaliserService.Normalise(
                    line!,
                    importExecutionContext.ImportParameters.ImportDataType,
                    importExecutionContext.Delimiter,
                    fileColumnCount);
                if (!string.Equals(nomalisedLine, line, StringComparison.Ordinal))
                {
                    amendedRowIds.Add(nomalisedLine.Split(importExecutionContext.Delimiter)[1]);
                    line = nomalisedLine;
                }
            }
            await writer.WriteLineAsync(line);
        }
        return amendedRowIds;
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