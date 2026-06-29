using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.Domain.Entities;
using Cads.Cds.StorageBridge.Core.Domain.Repositories;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;

public class S3SqlScriptExecutorService(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<S3SqlScriptExecutorService> logger) : IS3SqlScriptExecutorService
{
    private const string FileErrorSubDirectory = "data-seed/file-error";
    private const string FileProcessedSubDirectory = "data-seed/file-processed";

    private StorageBridgeWriteDbContext _dbContext = null!;
    private IStorageService<CadsInternalClient> _storageService = null!;
    private IFileChecksumService _checksumService = null!;
    private IDataSeedIngestionHistoryRepository _historyRepository = null!;

    [ExcludeFromCodeCoverage]
    public async Task<int> ExecuteAsync(CreateS3SqlImportJobDto job, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(job.SourceKey))
        {
            throw new ArgumentException("SourceKey must not be empty.", nameof(job));
        }

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Starting SQL script execution for prefix {SourceKey}", job.SourceKey);
        }

        await using var scope = serviceScopeFactory.CreateAsyncScope();

        _dbContext = scope.ServiceProvider.GetRequiredService<StorageBridgeWriteDbContext>();
        _storageService = scope.ServiceProvider.GetRequiredService<IStorageService<CadsInternalClient>>();
        _checksumService = scope.ServiceProvider.GetRequiredService<IFileChecksumService>();
        _historyRepository = scope.ServiceProvider.GetRequiredService<IDataSeedIngestionHistoryRepository>();

        var keys = await _storageService.ListKeysAsync(job.SourceKey, cancellationToken);
        var keyList = keys.ToList();

        if (keyList.Count == 0)
        {
            if (logger.IsEnabled(LogLevel.Warning))
            {
                logger.LogWarning("No SQL script files found under prefix {SourceKey}", job.SourceKey);
            }
            return 0;
        }

        var successCount = 0;
        var sw = Stopwatch.StartNew();

        foreach (var key in keyList)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var executed = await TryExecuteFileAsync(key, cancellationToken);
            if (executed) successCount++;
        }

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation(
                "Completed SQL script execution for prefix {SourceKey}. {SuccessCount}/{TotalCount} files succeeded in {ElapsedMs}ms",
                job.SourceKey, successCount, keyList.Count, sw.ElapsedMilliseconds);
        }

        return successCount;
    }

    [ExcludeFromCodeCoverage]
    private async Task<bool> TryExecuteFileAsync(string key, CancellationToken cancellationToken)
    {
        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Processing SQL script file {Key}", key);
        }

        try
        {
            var sql = await ReadSqlFromS3Async(key, cancellationToken);

            if (string.IsNullOrWhiteSpace(sql))
            {
                if (logger.IsEnabled(LogLevel.Warning))
                {
                    logger.LogWarning("SQL script file {Key} is empty — skipping.", key);
                }
                return false;
            }

            var existingRecord = await _historyRepository.GetByFileNameAsync(key, cancellationToken);

            if (existingRecord is not null)
            {
                return await HandleExistingFileAsync(existingRecord, key, cancellationToken);
            }

            // File not previously ingested — execute SQL
            await ExecuteSqlInTransactionAsync(sql, cancellationToken);

            // Record successful ingestion
            var checksum = await _checksumService.ComputeChecksumAsync(key, cancellationToken);
            await RecordIngestionHistoryAsync(key, checksum, cancellationToken);

            await MoveFileToDirectoryAsync(key, FileProcessedSubDirectory, cancellationToken);

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Successfully executed SQL script file {Key}", key);
            }

            return true;
        }
        catch (Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Failed to execute SQL script file {Key}: {Reason}", key, ex.Message);
            }
            return false;
        }
    }

    /// <summary>
    /// Handles the case where a history record already exists for the file.
    /// Returns false in all cases — either skipped (matching checksum) or quarantined (mismatched checksum).
    /// </summary>
    [ExcludeFromCodeCoverage]
    private async Task<bool> HandleExistingFileAsync(
        DataSeedIngestionHistory existingRecord,
        string key,
        CancellationToken cancellationToken)
    {
        var currentChecksum = await _checksumService.ComputeChecksumAsync(key, cancellationToken);

        if (string.Equals(currentChecksum, existingRecord.Checksum, StringComparison.OrdinalIgnoreCase))
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("SQL script file {Key} has already been applied and checksum matches — skipping.",
                    key);
            }
            return false;
        }

        if (logger.IsEnabled(LogLevel.Error))
        {
            logger.LogError(
                "SQL script file {Key} checksum mismatch. Expected: {ExpectedChecksum}, Actual: {ActualChecksum}. " +
                "File will be moved to error directory.",
                key, existingRecord.Checksum, currentChecksum);
        }

        await MoveFileToDirectoryAsync(key, FileErrorSubDirectory, cancellationToken);
        return false;
    }

    /// <summary>
    /// Moves an S3 file to the error subdirectory by copying then deleting the original.
    /// S3 has no native move; this is copy + delete.
    /// </summary>
    [ExcludeFromCodeCoverage]
    private async Task MoveFileToDirectoryAsync(string key, string fileSubDirectory, CancellationToken cancellationToken)
    {
        var fileName = Path.GetFileName(key);
        var destinationKey = $"{fileSubDirectory}/{fileName}";

        await _storageService.CopyAsync(key, destinationKey, cancellationToken);
        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Moved SQL script file {Key} to {DestinationKey}", key, destinationKey);
        }
    }

    /// <summary>
    /// Streams the S3 object and reads SQL text. The download is streamed from S3
    /// but the full string is materialised before PostgreSQL execution (driver requirement).
    /// </summary>
    [ExcludeFromCodeCoverage]
    private async Task<string> ReadSqlFromS3Async(string key, CancellationToken cancellationToken)
    {
        using var response = await _storageService.GetObjectResponseAsync(key, cancellationToken);

        if (response?.ResponseStream == null)
        {
            if (logger.IsEnabled(LogLevel.Warning))
            {
                logger.LogWarning("Null or missing response stream for SQL script file {Key}", key);
            }

            return string.Empty;
        }

        using var reader = new StreamReader(response.ResponseStream, leaveOpen: false);
        return await reader.ReadToEndAsync(cancellationToken);
    }

    [ExcludeFromCodeCoverage]
    private async Task ExecuteSqlInTransactionAsync(string sql, CancellationToken cancellationToken)
    {
        var connection = _dbContext.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = sql;
            command.CommandTimeout = 300;
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("[DATA-SEED-INGESTION] Executing DbCommand [CommandTimeout='{CommandTimeout}'] {CommandText}",
                    command.CommandTimeout, command.CommandText[..Math.Min(100, command.CommandText.Length)]);
            }
            var sw = Stopwatch.StartNew();
            var affected = await command.ExecuteNonQueryAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            sw.Stop();
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("[DATA-SEED-INGESTION] Executed DbCommand ({ElapsedMs}ms) — {RowsAffected} rows affected, transaction committed.",
                    sw.ElapsedMilliseconds, affected);
            }
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    [ExcludeFromCodeCoverage]
    private async Task RecordIngestionHistoryAsync(string key, string checksum, CancellationToken cancellationToken)
    {
        await _historyRepository.AddAsync(new DataSeedIngestionHistory
        {
            FileName = key,
            Checksum = checksum,
            AppliedAt = DateTimeOffset.UtcNow
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}