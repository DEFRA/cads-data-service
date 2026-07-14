using Cads.Cds.BuildingBlocks.Core.Domain.Imports;

namespace Cads.Cds.SystemAdmin.Controllers.Requests.Imports;

public class UpdateFileImportRequest
{
    public long TotalRowsToProcess { get; set; }
    public long RowsFound { get; set; }
    public FileImportStatus ImportStatus { get; set; }
}
