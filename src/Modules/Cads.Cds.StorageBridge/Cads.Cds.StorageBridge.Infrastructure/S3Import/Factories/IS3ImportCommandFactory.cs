using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;

public interface IS3ImportCommandFactory
{
    DbCommand CreateTempTableCommand(ImportDataType importDataType, SchemaName schemaName);

    StreamWriter CreateTextImport(ImportDataType importDataType, SchemaName schemaName, char delimiter, IEnumerable<string> columns);

    Task<DbCommand> CreateInsertCommandAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default);

    Task<DbCommand> CreateUpdateCommandAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default);

    Task<DbCommand> CreateUpsertCommandAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default);

    [ExcludeFromCodeCoverage]
    Task<DbCommand> CreateTempTableQueryCommandAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default);

    string GetTableName(ImportDataType importDataType, SchemaName schemaName, bool isTemp = false);

    Task<List<string>> FilterColumnsToTableAsync(ImportDataType importDataType, SchemaName schemaName, IEnumerable<string> fileColumns, CancellationToken cancellationToken = default);

    Task<List<string>> GetColumnNamesAsync(ImportDataType importDataType, SchemaName schemaName, CancellationToken cancellationToken = default);
}