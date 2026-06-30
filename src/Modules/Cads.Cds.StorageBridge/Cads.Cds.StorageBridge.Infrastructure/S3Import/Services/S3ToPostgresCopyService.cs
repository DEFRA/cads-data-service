using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Core.DTOs;
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
        ValidateJob(job);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Starting bulk import copy for job {JobId} with key SourceKey {SourceKey}",
                job.JobId, job.SourceKey);
        }

        await using var scope = serviceScopeFactory.CreateAsyncScope();

        _storageService = scope.ServiceProvider.GetRequiredService<IStorageService<CadsInternalClient>>();

        var keys = await _storageService.ListKeysAsync(job.SourceKey, cancellationToken);
        if (!keys.Any()) return 0;

        var dbContext = scope.ServiceProvider.GetRequiredService<StorageBridgeWriteDbContext>();
        var connection = await OpenConnectionAsync(dbContext, cancellationToken);

        var factoryProvider = scope.ServiceProvider.GetRequiredService<IS3ImportCommandFactoryProvider>();

        var factory = factoryProvider.Create((NpgsqlConnection)connection);
        var createTempTableCommand = factory.CreateTempTableCommand(job.ImportDataType, MapSchemaFromImportActionType(job.ImportActionType));
        var actionCommands = await GetCommandsAsync(job, factory, cancellationToken);

        var (counter, fileHistogram, batchHistogram) = S3ImportMetrics.CreateBulkLoadMetrics();

        var sw = Stopwatch.StartNew();
        var totalRows = 0;

        foreach (var key in keys)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Processing file {Key} for bulk import job {JobId}", key, job.JobId);
            }

            var fileSw = Stopwatch.StartNew();

            var rows = await ProcessFileAsync(
                key,
                job,
                factory,
                connection,
                createTempTableCommand,
                actionCommands,
                cancellationToken);

            totalRows += rows;
            counter.Add(rows);

            fileHistogram.Record(fileSw.Elapsed.TotalMilliseconds);

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Completed processing for file {Key} for bulk import job {JobId}, {TotalRows} records processed in {TotalMilliseconds} ms",
                    key, job.JobId, rows, fileSw.Elapsed.TotalMilliseconds);
            }
        }

        batchHistogram.Record(sw.Elapsed.TotalMilliseconds);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Completed bulk import copy for job {JobId} with key sourceKey {SourceKey}, {TotalRows} records processed in {TotalMilliseconds} ms",
                job.JobId, job.SourceKey, totalRows, sw.Elapsed.TotalMilliseconds);
        }

        return totalRows;
    }

    /// <summary>
    /// Cannot utilise low-level PostgreSQL/Persistence types using In Memory DB.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="job"></param>
    /// <param name="factory"></param>
    /// <param name="connection"></param>
    /// <param name="createTempTableCommand"></param>
    /// <param name="actionCommands"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [ExcludeFromCodeCoverage]
    private async Task<int> ProcessFileAsync(
        string key,
        CreateS3CsvImportJobDto job,
        IS3ImportCommandFactory factory,
        DbConnection connection,
        DbCommand createTempTableCommand,
        List<DbCommand> actionCommands,
        CancellationToken cancellationToken)
    {
        using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        await createTempTableCommand.ExecuteNonQueryAsync(cancellationToken);

        await CopyFileToStagingAsync(job.ImportDataType, MapSchemaFromImportActionType(job.ImportActionType), job.Delimiter, key, factory, cancellationToken);

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

        if (response?.ResponseStream == null)
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
        if (job.ImportActionType == ImportActionType.None)
            throw new InvalidOperationException("ImportType cannot be None.");

        if (string.IsNullOrWhiteSpace(job.SourceKey))
            throw new InvalidOperationException("SourceKey is required.");
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
        CreateS3CsvImportJobDto job,
        IS3ImportCommandFactory factory,
        CancellationToken cancellationToken)
    {
        var commands = new List<DbCommand>();
        var schemaName = MapSchemaFromImportActionType(job.ImportActionType);

        if (job.ImportActionType == ImportActionType.Bulk)
        {
            commands.Add(await factory.CreateUpsertCommandAsync(job.ImportDataType, schemaName, cancellationToken));
        }

        if (job.ImportActionType == ImportActionType.Transactional)
        {
            commands.Add(await factory.CreateInsertCommandAsync(job.ImportDataType, schemaName, cancellationToken));
        }

        return commands;
    }

    private static SchemaName MapSchemaFromImportActionType(ImportActionType importActionType)
    {
        if (importActionType == ImportActionType.Bulk)
        {
            return SchemaName.Cts;
        }

        if (importActionType == ImportActionType.Transactional)
        {
            return SchemaName.CtsTransactions;
        }

        return SchemaName.Public;
    }
}