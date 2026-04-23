using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Core.Services;
using Cads.Cds.StorageBridge.Infrastructure.Database.Factories;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Cads.Cds.StorageBridge.Infrastructure.Services;

public class BulkImportCopyService(
    StorageBridgeWriteDbContext dbContext,
    IStorageReader<CadsInternalClient> storageReader,
    ILogger<BulkImportCopyService> logger) : IBulkImportCopyService
{
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

            if (!keys.Any())
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("No objects found for job {jobId} with key prefix {prefix}.",
                        dto.JobId, dto.SourceKey);
                }

                return false;
            }

            var connection = dbContext.Database.GetDbConnection();

            //if (connection.State != ConnectionState.Open)
            //{
            //    await connection.OpenAsync(cancellationToken);
            //}

            //// Create a bulk import command factory to generate commands for the specified bulk import type
            //var bulkImportCommandFactory = new BulkImportCommandFactory(connection);

            var bulkImportCommandFactory = new BulkImportCommandFactory((NpgsqlConnection)connection);

            // Create a temporary table for the bulk import type
            var createTempTableCommand = bulkImportCommandFactory.CreateTempTableCommand(dto.BulkImportType);
            var setContraintStateOffCommand = bulkImportCommandFactory.CreateSetContraintStateCommand(dto.BulkImportType, false);
            var setContraintStateOnCommand = bulkImportCommandFactory.CreateSetContraintStateCommand(dto.BulkImportType, true);
            var upsertCommand = bulkImportCommandFactory.CreateUpsertCommand(dto.BulkImportType);
            var deleteCommand = bulkImportCommandFactory.CreateDeleteCommand(dto.BulkImportType);

            // Process each file found under the specified bucket and prefix
            foreach (var key in keys)
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("Processing file {key} for bulk import job {jobId}", key, dto.JobId);
                }

                var transaction = await connection.BeginTransactionAsync(cancellationToken);

                await createTempTableCommand.ExecuteNonQueryAsync(cancellationToken);
                await setContraintStateOffCommand.ExecuteNonQueryAsync(cancellationToken);

                await CopyFileToStagingAsync(bulkImportCommandFactory, dto.BulkImportType, dto.Delimiter, key, cancellationToken);

                await upsertCommand.ExecuteNonQueryAsync(cancellationToken);
                await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
                await setContraintStateOnCommand.ExecuteNonQueryAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Completed bulk import copy for job {jobId} with key prefix {prefix}",
                    dto.JobId, dto.SourceKey);
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

    private async Task CopyFileToStagingAsync(
        IBulkImportCommandFactory commandFactory,
        BulkImportType bulkImportType,
        char delimiter,
        string key,
        CancellationToken cancellationToken = default)
    {
        // Get a stream reader for the specified key from storage
        using var reader = await storageReader.GetStreamReader(key, cancellationToken);

        // Create a text import writer for the bulk import type and delimiter
        using var writer = commandFactory.CreateTextImport(bulkImportType, delimiter);

        // Read each line from the input stream and write it to the text import writer
        string? line;
        while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
        {
            await writer.WriteLineAsync(line);
        }
    }
}
