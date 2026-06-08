using Cads.Cds.StorageBridge.Core.Domain.Enums;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;

public interface IS3BulkLoadCommandFactory
{
    DbCommand CreateTempTableCommand(BulkLoadDataType bulkImportType);

    StreamWriter CreateTextImport(BulkLoadDataType bulkImportType, char delimiter, IEnumerable<string> columns);

    Task<DbCommand> CreateInsertCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default);

    Task<DbCommand> CreateUpdateCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default);

    Task<DbCommand> CreateUpsertCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default);

    Task<DbCommand> CreateDeleteCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default);

    [ExcludeFromCodeCoverage]
    Task<DbCommand> CreateTempTableQueryCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default);

    string GetTableName(BulkLoadDataType bulkImportType, bool isTemp = false);

    Task<List<string>> FilterColumnsToTableAsync(BulkLoadDataType bulkImportType, IEnumerable<string> fileColumns, CancellationToken cancellationToken = default);

    Task<List<string>> GetColumnNamesAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default);
}