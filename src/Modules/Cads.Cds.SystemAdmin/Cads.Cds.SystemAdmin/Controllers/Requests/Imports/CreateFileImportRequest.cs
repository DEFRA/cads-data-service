namespace Cads.Cds.SystemAdmin.Controllers.Requests.Imports;

public class CreateFileImportRequest
{
    public string DestinationTableName { get; set; } = default!;
    public string FileName { get; set; } = default!;

    public long TotalRowsToProcess { get; set; }
    public long RowsFound { get; set; }
}