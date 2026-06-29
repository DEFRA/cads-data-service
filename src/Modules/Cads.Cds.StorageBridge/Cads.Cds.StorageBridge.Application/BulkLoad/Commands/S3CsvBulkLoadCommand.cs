using Cads.Cds.StorageBridge.Application.Commands;
using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Application.BulkLoad.Commands;

public class S3CsvBulkLoadCommand : IStorageBridgeCommand<Guid>
{
    public string SourceKey { get; set; } = string.Empty;

    public BulkLoadDataType BulkImportType { get; set; }

    public ImportActions ActionType { get; set; }

    public char Delimiter { get; set; }
}