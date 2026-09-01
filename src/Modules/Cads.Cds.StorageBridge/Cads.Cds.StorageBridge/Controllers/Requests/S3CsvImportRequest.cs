using Cads.Cds.ApiSurface.Dtos.Imports;

namespace Cads.Cds.StorageBridge.Controllers.Requests;

public class S3CsvImportRequest
{
    public long? FileImportId { get; set; }

    public string? SourceKey { get; set; }

    public char Delimiter { get; set; } = '|';

    public FileImportStatus? ForceResetImportStatus { get; set; }
}