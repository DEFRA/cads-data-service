namespace Cads.Cds.SystemAdmin.Controllers.Requests.Imports;

public class CreateFileImportRequest
{
    public string FileName { get; set; } = default!;

    /// <summary>
    /// The internal bucket prefix the file was copied to, e.g. <c>import/cts/bulk</c>.
    /// The Storage Bridge reads the file and its split parts from under this prefix.
    /// </summary>
    public string DestinationPrefix { get; set; } = default!;

    public long? TotalRowsToProcess { get; set; }
    public long? RowsFound { get; set; }
}