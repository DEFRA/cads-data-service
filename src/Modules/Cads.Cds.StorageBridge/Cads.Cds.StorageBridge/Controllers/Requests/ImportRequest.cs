using Cads.Cds.StorageBridge.Core.Domain;

namespace Cads.Cds.StorageBridge.Controllers.Requests;

public class ImportRequest
{
    public required string SourceKey { get; set; }

    public required BulkImportType BulkImportType { get; set; }

    public char Delimiter { get; set; } = '|';
}