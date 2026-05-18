using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Services;

public class S3ToPostgresCopyService(
    IServiceScopeFactory scopeFactory,
    IStorageReader<CadsInternalClient> storageReader,
    ILogger<S3ToPostgresCopyService> logger) : IS3ToPostgresCopyService
{
    public async Task<bool> ExecuteAsync(CreateS3BulkLoadJobDto job, CancellationToken cancellationToken = default)
    {
        ValidateJob(job);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Starting bulk import copy for job {jobId} with key sourceKey {sourceKey}",
                job.JobId, job.SourceKey);
        }

        var keys = await storageReader.ListKeysAsync(job.SourceKey, cancellationToken);
        if (!keys.Any()) return false;

        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<StorageBridgeWriteDbContext>();
        var connection = await OpenConnectionAsync(dbContext, cancellationToken);

        var factory = new S3BulkLoadCommandFactory((NpgsqlConnection)connection);
        var createTempTableCommand = factory.CreateTempTableCommand(job.BulkImportType);
        var actionCommands = await GetCommandsAsync(job, factory, cancellationToken);

        var (counter, fileHistogram, batchHistogram) = CreateMetrics();

        var sw = Stopwatch.StartNew();
        var totalRows = 0;

        foreach (var key in keys)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Processing file {key} for bulk import job {jobId}", key, job.JobId);
            }

            var fileSw = Stopwatch.StartNew();

            var rows = await ProcessFileAsync(
                key,
                job,
                factory,
                createTempTableCommand,
                actionCommands,
                cancellationToken);

            totalRows += rows;
            counter.Add(rows);

            fileHistogram.Record(fileSw.Elapsed.TotalMilliseconds);

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Completed processing for file {key} for bulk import job {jobId}, {totalRows} records processed in {totalMilliseconds} ms",
                    key, job.JobId, rows, fileSw.Elapsed.TotalMilliseconds);
            }
        }

        batchHistogram.Record(sw.Elapsed.TotalMilliseconds);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Completed bulk import copy for job {jobId} with key sourceKey {sourceKey}, {totalRows} records processed in {totalMilliseconds} ms",
                job.JobId, job.SourceKey, totalRows, sw.Elapsed.TotalMilliseconds);
        }

        return true;
    }

    private async Task<int> ProcessFileAsync(
        string key,
        CreateS3BulkLoadJobDto job,
        S3BulkLoadCommandFactory factory,
        DbCommand createTempTableCommand,
        List<DbCommand> actionCommands,
        CancellationToken cancellationToken)
    {
        using var transaction = await factory.Connection.BeginTransactionAsync(cancellationToken);

        await createTempTableCommand.ExecuteNonQueryAsync(cancellationToken);

        await CopyFileToStagingAsync(job.BulkImportType, job.Delimiter, key, factory, cancellationToken);

        var rows = await ExecuteActionCommandsAsync(actionCommands, cancellationToken);

        await transaction.CommitAsync(cancellationToken);

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

            if (command == null) continue;

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Command: {commandText}", command.CommandText);
            }

            total += await command.ExecuteNonQueryAsync(cancellationToken);
        }

        return total;
    }

    private async Task CopyFileToStagingAsync(
        BulkLoadDataType bulkLoadDataType,
        char delimiter,
        string key,
        S3BulkLoadCommandFactory factory,
        CancellationToken cancellationToken)
    {
        using var response = await storageReader.GetObjectResponseAsync(key, cancellationToken);

        if (response?.ResponseStream == null)
        {
            logger.LogWarning("Null stream for key {key}", key);
            return;
        }

        using var reader = new StreamReader(response.ResponseStream);

        //var header = await reader.ReadLineAsync(cancellationToken) ?? throw new InvalidOperationException($"File {key} is empty or missing header row.");
        //var columns = header.Split(delimiter) ?? throw new InvalidOperationException("Missing header row");
        //using var writer = factory.CreateTextImport(bulkLoadDataType, delimiter, columns);

        var header = await reader.ReadLineAsync(cancellationToken)
            ?? throw new InvalidOperationException($"File {key} is empty or missing header row.");

        var fileColumns = header.Split(delimiter);

        var matchedColumns = await factory.FilterColumnsToTableAsync(
            bulkLoadDataType,
            fileColumns,
            cancellationToken);

        using var writer = factory.CreateTextImport(bulkLoadDataType, delimiter, matchedColumns);

        string? line;
        while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (line.StartsWith("T|")) break;
            await writer.WriteLineAsync(SanitiseLine(line));
        }
    }

    private static (Counter<int> counter, Histogram<double> fileHistogram, Histogram<double> batchHistogram) CreateMetrics()
    {
        var meter = new Meter("Cads.Postgres.Metrics", "1.0");

        var counter = meter.CreateCounter<int>("cads_batch_import_rows_affected", "rows");
        var fileHistogram = meter.CreateHistogram<double>("postgres_file_import_duration_ms", "ms");
        var batchHistogram = meter.CreateHistogram<double>("postgres_batch_import_duration_ms", "ms");

        return (counter, fileHistogram, batchHistogram);
    }

    private static string SanitiseLine(string? line)
    {
        var sanitisedResult = line ?? string.Empty;

        sanitisedResult = sanitisedResult.Replace("\"", "\"\"");

        sanitisedResult = Regex.Replace(
            sanitisedResult,
            @"[\u0000-\u001F]",
            " ",
            RegexOptions.None,
            TimeSpan.FromMicroseconds(50));

        return sanitisedResult;
    }

    private static void ValidateJob(CreateS3BulkLoadJobDto job)
    {
        if (job.ImportActionType == ImportActionType.None)
            throw new InvalidOperationException("ImportActionType cannot be None.");

        if (string.IsNullOrWhiteSpace(job.SourceKey))
            throw new InvalidOperationException("SourceKey is required.");
    }

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
        CreateS3BulkLoadJobDto job,
        S3BulkLoadCommandFactory factory,
        CancellationToken cancellationToken)
    {
        var commands = new List<DbCommand>();
        var action = job.ImportActionType;

        if (action == ImportActionType.None)
            return commands;

        var insert = action.HasFlag(ImportActionType.Insert);
        var update = action.HasFlag(ImportActionType.Update);
        var delete = action.HasFlag(ImportActionType.Delete);

        if (insert && update)
        {
            commands.Add(await factory.CreateUpsertCommandAsync(job.BulkImportType, cancellationToken));
            return commands;
        }

        if (insert)
        {
            commands.Add(await factory.CreateInsertCommandAsync(job.BulkImportType, cancellationToken));
            return commands;
        }

        if (update)
        {
            commands.Add(await factory.CreateUpdateCommandAsync(job.BulkImportType, cancellationToken));
            return commands;
        }

        if (delete)
        {
            commands.Add(await factory.CreateDeleteCommandAsync(job.BulkImportType, cancellationToken));
            return commands;
        }

        return commands;
    }
}