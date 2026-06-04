using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Controllers.Requests;

public class S3CsvBulkLoadRequest
{
    public required string SourceKey { get; set; }

    public required BulkLoadDataType BulkImportType { get; set; }

    public ImportActions ActionType { get; set; } = ImportActions.Insert | ImportActions.Update;

    public char Delimiter { get; set; } = '|';
}