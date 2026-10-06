using Cads.Cds.ApiSurface.Dtos.Imports;

namespace Cads.Cds.SystemAdmin.Controllers.Requests.Imports;

public class UpdateFileImportRequest
{
    public long? TotalRowsToProcess { get; set; }
    public long? RowsFound { get; set; }
    public long? RowsImported { get; set; }
    public string? LastFilePartImported { get; set; }
    public FileImportStatus? ImportStatus { get; set; }
}