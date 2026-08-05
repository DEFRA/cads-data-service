namespace Cads.Cds.BuildingBlocks.Core.Domain.Imports;

public class FileImportCreate
{
    public string? DestinationTableName { get; set; }
    public string? FileName { get; set; }
    public long TotalRowsToProcess { get; set; }
    public long RowsFound { get; set; }
    public string? GroupKey { get; set; }
    public string? ImportType { get; set; }
    public DateTimeOffset BatchDate { get; set; }
}