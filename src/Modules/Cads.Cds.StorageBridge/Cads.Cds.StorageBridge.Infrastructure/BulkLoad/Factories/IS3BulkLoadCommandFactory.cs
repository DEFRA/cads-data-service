using Cads.Cds.StorageBridge.Core.Domain.Enums;
using System.Data.Common;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;

public interface IS3BulkLoadCommandFactory
{
    DbCommand CreateTempTableCommand(BulkLoadDataTypes bulkImportType);

    StreamWriter CreateTextImport(BulkLoadDataTypes bulkImportType, char delimiter, IEnumerable<string> columns);

    Task<DbCommand> CreateInsertCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default);

    Task<DbCommand> CreateUpdateCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default);

    Task<DbCommand> CreateUpsertCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default);

    Task<DbCommand> CreateDeleteCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default);

    string GetTableName(BulkLoadDataTypes bulkImportType, bool isTemp = false);

    Task<List<string>> FilterColumnsToTableAsync(BulkLoadDataTypes bulkImportType, IEnumerable<string> fileColumns, CancellationToken cancellationToken = default);

    Task<List<string>> GetColumnNamesAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default);

    Task<DbCommand> CreateTempTableQueryCommandAsync(BulkLoadDataTypes bulkImportType, CancellationToken cancellationToken = default);
}