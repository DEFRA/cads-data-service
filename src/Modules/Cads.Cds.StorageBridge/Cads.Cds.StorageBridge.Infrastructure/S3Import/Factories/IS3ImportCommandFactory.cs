using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using System.Data.Common;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;

public interface IS3ImportCommandFactory
{
    DbCommand CreateTempTableCommand(ImportDataType importDataType, SchemaName schemaName, long? fileImportId);

    StreamWriter CreateTextImport(ImportDataType importDataType, SchemaName schemaName, char delimiter, IEnumerable<string> columns);

    Task<DbCommand> CreateInsertCommandAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default);

    Task<DbCommand> CreateUpdateCommandAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default);

    Task<DbCommand> CreateUpsertCommandAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default);

    Task<List<string>> FilterColumnsToTableAsync(ImportDataType importDataType, SchemaName schemaName, IEnumerable<string> fileColumns, CancellationToken cancellationToken = default);

    Task<List<string>> GetColumnNamesAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default);
}