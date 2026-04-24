using Cads.Cds.StorageBridge.Core.Domain.Enums;
using System.Data.Common;

namespace Cads.Cds.StorageBridge.Infrastructure.Database.Factories;

public interface IBulkImportCommandFactory
{
    DbCommand CreateSetContraintStateCommand(BulkImportType bulkImportType, bool state);
    DbCommand CreateDeleteCommand(BulkImportType bulkImportType);
    DbCommand CreateTempTableCommand(BulkImportType bulkImportType);
    StreamWriter CreateTextImport(BulkImportType bulkImportType, char delimiter);
    DbCommand CreateUpsertCommand(BulkImportType bulkImportType);
    DbCommand CreateSetDeferredAllContraintCommand();

    DbCommand CreateTempTableQueryCommand(BulkImportType bulkImportType);
}