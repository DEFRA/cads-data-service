using Cads.Cds.StorageBridge.Core.Domain.Enums;
using System.Data.Common;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;

public interface IS3BulkLoadCommandFactory
{
    DbCommand CreateSetContraintStateCommand(BulkLoadDataType bulkImportType, bool state);
    Task<DbCommand> CreateDeleteCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default);
    DbCommand CreateTempTableCommand(BulkLoadDataType bulkImportType);
    StreamWriter CreateTextImport(BulkLoadDataType bulkImportType, char delimiter);
    StreamWriter CreateTextImport(BulkLoadDataType bulkImportType, char delimiter, IEnumerable<string> columns);
    Task<DbCommand> CreateUpsertCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default);
    DbCommand CreateSetDeferredAllContraintCommand();
    Task<DbCommand> CreateTempTableQueryCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default);
    Task<DbCommand> CreateInsertCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default);
    Task<DbCommand> CreateUpdateCommandAsync(BulkLoadDataType bulkImportType, CancellationToken cancellationToken = default);
    DbCommand CreateReindexCommand(BulkLoadDataType bulkImportType);
}