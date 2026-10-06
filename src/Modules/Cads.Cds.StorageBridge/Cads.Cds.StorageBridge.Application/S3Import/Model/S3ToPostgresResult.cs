namespace Cads.Cds.StorageBridge.Application.S3Import.Model;

public class S3ToPostgresResult
{
    public long TotalRowsProcessed { get; set; }
    public List<string> RowIdsAmended { get; set; } = new List<string>();
}