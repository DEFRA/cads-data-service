using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;

public interface IS3ImportCommandFactory
{
    DbCommand CreateTempTableCommand(ImportDataType bulkImportType, SchemaName schemaName);

    StreamWriter CreateTextImport(ImportDataType bulkImportType, SchemaName schemaName, char delimiter, IEnumerable<string> columns);

    Task<DbCommand> CreateInsertCommandAsync(ImportDataType bulkImportType, SchemaName schemaName, CancellationToken cancellationToken = default);

    Task<DbCommand> CreateUpdateCommandAsync(ImportDataType bulkImportType, SchemaName schemaName, CancellationToken cancellationToken = default);

    Task<DbCommand> CreateUpsertCommandAsync(ImportDataType bulkImportType, SchemaName schemaName, CancellationToken cancellationToken = default);

    Task<DbCommand> CreateDeleteCommandAsync(ImportDataType bulkImportType, SchemaName schemaName, CancellationToken cancellationToken = default);

    [ExcludeFromCodeCoverage]
    Task<DbCommand> CreateTempTableQueryCommandAsync(ImportDataType bulkImportType, SchemaName schemaName, CancellationToken cancellationToken = default);

    string GetTableName(ImportDataType bulkImportType, SchemaName schemaName, bool isTemp = false);

    Task<List<string>> FilterColumnsToTableAsync(ImportDataType bulkImportType, SchemaName schemaName, IEnumerable<string> fileColumns, CancellationToken cancellationToken = default);

    Task<List<string>> GetColumnNamesAsync(ImportDataType bulkImportType, SchemaName schemaName, CancellationToken cancellationToken = default);
}