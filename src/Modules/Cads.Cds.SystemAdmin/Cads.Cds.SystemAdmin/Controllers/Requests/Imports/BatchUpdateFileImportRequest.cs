using Cads.Cds.ApiSurface.Dtos.Imports;

namespace Cads.Cds.SystemAdmin.Controllers.Requests.Imports;

public class BatchUpdateFileImportRequest
{
    public required string GroupKey { get; set; }

    public long? TotalRowsToProcess { get; set; }

    public long? RowsFound { get; set; }

    public FileImportStatus? ImportStatus { get; set; }
}