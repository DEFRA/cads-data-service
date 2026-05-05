using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Core.Services;
using Cads.Cds.StorageBridge.Infrastructure.Database.Factories;
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

namespace Cads.Cds.StorageBridge.Infrastructure.Services;

public class BulkImportCopyService(
    IServiceScopeFactory scopeFactory,
    IStorageReader<CadsInternalClient> storageReader,
    ILogger<BulkImportCopyService> logger) : IBulkImportCopyService
{
    private IBulkImportCommandFactory? _commandFactory;

    private bool QueryTestData => false; // Set to true to enable temporary table data logging for debugging purposes, should be false for production use to avoid unnecessary overhead

    public async Task<bool> ExecuteAsync(CreateBulkImportJobDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Starting bulk import copy for job {jobId} with key prefix {prefix}",
                    dto.JobId, dto.SourceKey);
            }

            // List all keys under the specified bucket and prefix
            var keys = await storageReader.ListKeysAsync(dto.SourceKey, cancellationToken);

            if (keys == null || !keys.Any())
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("No objects found for job {jobId} with key prefix {prefix}.",
                        dto.JobId, dto.SourceKey);
                }

                return false;
            }

            if (dto.ImportActionType == ImportActionType.None)
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("No import action defined for job {jobId} with key prefix {prefix}.",
                        dto.JobId, dto.SourceKey);
                }

                return false;
            }

            using var scope = scopeFactory.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<StorageBridgeWriteDbContext>();

            var connection = dbContext.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync(cancellationToken);
            }

            // Create a bulk import command factory to generate commands for the specified bulk import type
            _commandFactory = new BulkImportCommandFactory((NpgsqlConnection)connection);

            if (_commandFactory == null)
            {
                throw new InvalidOperationException("Failed to create bulk import command factory.");
            }

            // Create a temporary table for the bulk import type
            var createTempTableCommand = _commandFactory.CreateTempTableCommand(dto.BulkImportType);

            if (createTempTableCommand == null)
            {
                throw new InvalidOperationException("One or more required database commands could not be created.");
            }

            var actionCommands = await GetCommandsAsync(dto, cancellationToken) ?? new List<DbCommand>();

            var meter = new Meter("Cads.Postgres.Metrics", "1.0");

            var counter = meter.CreateCounter<int>(
                name: "cads_batch_import_rows_affected",
                unit: "rows",
                description: "Number of rows processed");

            var batchImportDurationHistogram = meter.CreateHistogram<double>(
                name: "postgres_batch_import_duration_ms",
                unit: "ms",
                description: "batch import duration");

            var fileImportDurationHistogram = meter.CreateHistogram<double>(
                name: "postgres_file_import_duration_ms",
                unit: "ms",
                description: "file import duration");

            var sw = Stopwatch.StartNew();
            var totalRowsAffected = 0;

            if (dto.UpdateConstraints)
            {
                await _commandFactory.CreateSetContraintStateCommand(dto.BulkImportType, false).ExecuteNonQueryAsync(cancellationToken);
            }

            // Process each file found under the specified bucket and prefix
            foreach (var key in keys)
            {
                if (key == null)
                {
                    // skip null keys defensively
                    continue;
                }

                var fsw = Stopwatch.StartNew();

                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("Processing file {key} for bulk import job {jobId}", key, dto.JobId);
                }

                var transaction = await connection.BeginTransactionAsync(cancellationToken);

                await createTempTableCommand.ExecuteNonQueryAsync(cancellationToken);

                await CopyFileToStagingAsync(dto.BulkImportType, dto.Delimiter, key, cancellationToken);

                var rowsAffected = 0;

                foreach (var actionCommand in actionCommands)
                {
                    if (actionCommand == null) continue;
                    rowsAffected += await actionCommand.ExecuteNonQueryAsync(cancellationToken);
                }

                if (QueryTestData && rowsAffected > 0)
                {
                    var tempData = await GetTempDataAsync(dto, cancellationToken);
                    if (logger.IsEnabled(LogLevel.Information))
                    {
                        logger.LogInformation("Temporary table populated with {recordCount} records for job {jobId} with key prefix {prefix}",
                            tempData?.Tables[0].Rows.Count, dto.JobId, dto.SourceKey);
                    }
                }

                counter.Add(rowsAffected);

                totalRowsAffected += rowsAffected;


                await transaction.CommitAsync(cancellationToken);

                fsw.Stop();
                var fileImportQueryElapsed = fsw.Elapsed.TotalMilliseconds;
                fileImportDurationHistogram.Record(fileImportQueryElapsed,
                    new KeyValuePair<string, object?>("job", dto.JobId),
                    new KeyValuePair<string, object?>("key", key),
                    new KeyValuePair<string, object?>("rows", rowsAffected));

                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("Completed processing for file {key} for bulk import job {jobId}, {rowsAffected} records processed in {ElapsedMilliseconds} ms",
                        key, dto.JobId, rowsAffected, fileImportQueryElapsed);
                }
            }

            if (dto.UpdateConstraints)
            {
                await _commandFactory.CreateSetContraintStateCommand(dto.BulkImportType, true).ExecuteNonQueryAsync(cancellationToken);
                await _commandFactory.CreateReindexCommand(dto.BulkImportType).ExecuteNonQueryAsync(cancellationToken);
            }

            sw.Stop();
            var batchImportElapsed = sw.Elapsed.TotalMilliseconds;
            batchImportDurationHistogram.Record(batchImportElapsed,
                new KeyValuePair<string, object?>("job", dto.JobId),
                new KeyValuePair<string, object?>("rows", totalRowsAffected));

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Completed bulk import copy for job {jobId} with key prefix {prefix}, {totalRowsAffected} records processed in {ElapsedMilliseconds} ms",
                    dto.JobId, dto.SourceKey, totalRowsAffected, batchImportElapsed);
            }
        }
        catch (Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Error occurred during bulk import copy for job {jobId} with key prefix {prefix}",
                    dto.JobId, dto.SourceKey);
            }

            throw;
        }

        return true;
    }

    private async Task<DataSet?> GetTempDataAsync(CreateBulkImportJobDto dto, CancellationToken cancellationToken = default)
    {
        // Ensure command factory is available
        if (_commandFactory == null)
        {
            throw new InvalidOperationException("Command factory is not initialized.");
        }

        // Generates a DbCommand to query the temporary table for the specified bulk import type.
        // This command is used to load the data into memory before performing upsert/delete operations.
        // The command is created using the bulk import command factory and is specific to the database schema for the bulk import type.
        var command = await _commandFactory.CreateTempTableQueryCommandAsync(dto.BulkImportType, cancellationToken);

        if (command == null)
        {
            return null;
        }

        // Temporarily execute the query command to load the data into a DataSet.
        // This is necessary to ensure that the data is loaded into memory before we turn off constraints and perform upsert/delete operations.
        var data = ExecuteQueryToDataSet(command);

        return data;
    }

    private async Task<List<DbCommand>> GetCommandsAsync(CreateBulkImportJobDto dto, CancellationToken cancellationToken = default)
    {
        var commands = new List<DbCommand>();

        switch (dto.ImportActionType)
        {
            case ImportActionType.None:
                return commands;

            case ImportActionType.Insert:
                {
                    var cmd = await _commandFactory!.CreateInsertCommandAsync(dto.BulkImportType, cancellationToken);
                    if (cmd != null) commands.Add(cmd);
                    break;
                }

            case ImportActionType.Update:
                {
                    var cmd = await _commandFactory!.CreateUpdateCommandAsync(dto.BulkImportType, cancellationToken);
                    if (cmd != null) commands.Add(cmd);
                    break;
                }

            case ImportActionType.Delete:
                // Removed for now, unsure how we will handle deletes
                //commands.Add(await bulkImportCommandFactory.CreateDeleteCommandAsync(dto.BulkImportType, cancellationToken));
                break;

            case var a when (a & ImportActionType.Insert) != 0 && (a & ImportActionType.Update) != 0:
                {
                    var cmd = await _commandFactory!.CreateUpsertCommandAsync(dto.BulkImportType, cancellationToken);
                    if (cmd != null) commands.Add(cmd);
                    break;
                }

            case var a when (a & ImportActionType.Insert) != 0 && (a & ImportActionType.Update) != 0 && (a & ImportActionType.Delete) != 0:
                {
                    var cmd = await _commandFactory!.CreateUpsertCommandAsync(dto.BulkImportType, cancellationToken);
                    if (cmd != null) commands.Add(cmd);
                    // Removed for now, unsure how we will handle deletes
                    // commands.Add(await bulkImportCommandFactory.CreateDeleteCommandAsync(dto.BulkImportType, cancellationToken));
                    break;
                }
        }

        return commands;
    }

    private async Task CopyFileToStagingAsync(
        BulkImportType bulkImportType,
        char delimiter,
        string key,
        CancellationToken cancellationToken = default)
    {
        // Ensure command factory is available
        if (_commandFactory == null)
        {
            throw new InvalidOperationException("Command factory is not initialized.");
        }

        // Get a stream reader for the specified key from storage
        using var response = await storageReader.GetObjectResponseAsync(key, cancellationToken);
        if (response?.ResponseStream == null)
        {
            if (logger.IsEnabled(LogLevel.Warning))
            {
                logger.LogWarning("Storage response or response stream was null for key {key}", key);
            }
            return;
        }

        using var streamReader = new StreamReader(response.ResponseStream);

        // Create a text import writer for the bulk import type and delimiter
        using var writer = _commandFactory.CreateTextImport(bulkImportType, delimiter);

        // Read each line from the input stream and write it to the text import writer
        string? line;
        while ((line = await streamReader.ReadLineAsync(cancellationToken)) != null)
        {
            if (line.StartsWith("T|")) // Skip the separator definition line if it exists
            {
                break;
            }
            await writer.WriteLineAsync(line);
        }
    }

    private static DataSet ExecuteQueryToDataSet(DbCommand command)
    {
        // Creates a DataSet and fills it with the results of the provided DbCommand.
        // Uses NpgsqlDataAdapter to execute the command and populate the DataSet in-memory.
        // Returned DataSet must be disposed by the caller when no longer needed.
        // Exceptions from the adapter/command will bubble up to the caller.
        var dataSet = new DataSet();

        using (var adapter = new NpgsqlDataAdapter((NpgsqlCommand)command))
        {
            adapter.Fill(dataSet);
        }

        return dataSet;
    }
}