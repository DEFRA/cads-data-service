using System.Data;
using System.Data.Common;
using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Models;

public sealed record ImportExecutionContext(
    FileImport FileImport,
    ImportParameters ImportParameters,
    char Delimiter,
    IS3ImportCommandFactory Factory,
    IDefensiveCopyLineNormaliser DefensiveCopyLineNormaliser,
    StorageBridgeWriteDbContext DbContext,
    DbCommand CreateTempTableCommand,
    IReadOnlyList<DbCommand> ActionCommands)
{
    public static async Task<ImportExecutionContext> CreateAsync(
        FileImport fileImport,
        char delimiter,
        IServiceProvider serviceProvider,
        CancellationToken cancellationToken)
    {
        var importParameters = fileImport.FileName.GetImportParameters();
        var dbContext = serviceProvider.GetRequiredService<StorageBridgeWriteDbContext>();

        var connection = await OpenConnectionAsync(dbContext, cancellationToken);

        var factoryProvider = serviceProvider.GetRequiredService<IS3ImportCommandFactoryProvider>();
        var factory = factoryProvider.Create((NpgsqlConnection)connection);
        
        var defensiveCopyLineNormaliser = serviceProvider.GetRequiredService<IDefensiveCopyLineNormaliser>();

        var createTempTableCommand = factory.CreateTempTableCommand(
            importParameters.ImportDataType,
            importParameters.SchemaName,
            importParameters.ImportActionType,
            fileImport.Id);

        var actionCommands = await GetCommandsAsync(
            importParameters.ImportDataType,
            importParameters.SchemaName,
            importParameters.ImportActionType,
            factory,
            cancellationToken);

        return new ImportExecutionContext(
            fileImport,
            importParameters,
            delimiter,
            factory,
            defensiveCopyLineNormaliser,
            dbContext,
            createTempTableCommand,
            actionCommands);
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
}