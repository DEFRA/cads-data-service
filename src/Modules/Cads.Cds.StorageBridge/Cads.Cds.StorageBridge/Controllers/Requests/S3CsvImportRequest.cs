namespace Cads.Cds.StorageBridge.Controllers.Requests;

public class S3CsvImportRequest
{
    public required string SourceKey { get; set; }

    public char Delimiter { get; set; } = '|';
}