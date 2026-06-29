using Cads.Cds.StorageBridge.Core.Domain.Enums;

namespace Cads.Cds.StorageBridge.Controllers.Requests;

public class S3CsvImportRequest
{
    public required string SourceKey { get; set; }

    public required ImportDataType ImportDataType { get; set; }

    public ImportActionType ImportActionType { get; set; }

    public char Delimiter { get; set; } = '|';
}