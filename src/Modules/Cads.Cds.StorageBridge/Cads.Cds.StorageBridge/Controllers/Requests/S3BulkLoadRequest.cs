using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Controllers.Requests;

public class S3BulkLoadRequest
{
    public required string SourceKey { get; set; }

    public required BulkLoadDataType BulkImportType { get; set; }

    public ImportActionType ActionType { get; set; } = ImportActionType.Insert | ImportActionType.Update;

    public char Delimiter { get; set; } = '|';
}