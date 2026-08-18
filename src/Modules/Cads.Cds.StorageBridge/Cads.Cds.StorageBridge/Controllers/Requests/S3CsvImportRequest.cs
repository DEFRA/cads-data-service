namespace Cads.Cds.StorageBridge.Controllers.Requests;

public class S3CsvImportRequest
{
    public long? FileImportId { get; set; }

    public string? SourceKey { get; set; }

    public char Delimiter { get; set; } = '|';
}