using Cads.Cds.StorageBridge.Core.Domain.Enums;
using System.Data.Common;

namespace Cads.Cds.StorageBridge.Infrastructure.Database.Factories;

public interface IBulkImportCommandFactory
{
    DbCommand CreateSetContraintStateCommand(BulkImportType bulkImportType, bool state);
    Task<DbCommand> CreateDeleteCommandAsync(BulkImportType bulkImportType, CancellationToken cancellationToken = default);
    DbCommand CreateTempTableCommand(BulkImportType bulkImportType);
    StreamWriter CreateTextImport(BulkImportType bulkImportType, char delimiter);
    StreamWriter CreateTextImport(BulkImportType bulkImportType, char delimiter, IEnumerable<string> columns);
    Task<DbCommand> CreateUpsertCommandAsync(BulkImportType bulkImportType, CancellationToken cancellationToken = default);
    DbCommand CreateSetDeferredAllContraintCommand();
    Task<DbCommand> CreateTempTableQueryCommandAsync(BulkImportType bulkImportType, CancellationToken cancellationToken = default);
    Task<DbCommand> CreateInsertCommandAsync(BulkImportType bulkImportType, CancellationToken cancellationToken = default);
    Task<DbCommand> CreateUpdateCommandAsync(BulkImportType bulkImportType, CancellationToken cancellationToken = default);
    DbCommand CreateReindexCommand(BulkImportType bulkImportType);
}