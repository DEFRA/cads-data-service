using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Controllers.Requests;

public class ImportRequest
{
    public required string SourceKey { get; set; }

    public required BulkImportType BulkImportType { get; set; }

    public ImportActionType ActionType { get; set; } = ImportActionType.Insert | ImportActionType.Update;

    public char Delimiter { get; set; } = '|';
}