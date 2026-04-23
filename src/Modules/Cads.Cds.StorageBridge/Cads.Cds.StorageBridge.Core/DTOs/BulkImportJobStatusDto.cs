namespace Cads.Cds.StorageBridge.Core.DTOs;

public class BulkImportJobStatusDto
{
    public string SourceKey { get; set; } = string.Empty;

    public string DestinationTableName { get; set; } = string.Empty;

    public string Delimiter { get; set; } = string.Empty;
}