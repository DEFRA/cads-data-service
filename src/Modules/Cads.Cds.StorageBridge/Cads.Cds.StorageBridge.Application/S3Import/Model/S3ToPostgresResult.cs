namespace Cads.Cds.StorageBridge.Application.S3Import.Model;

public class S3ToPostgresResult
{
    public long TotalRowsProcessed { get; set; }
    public List<string> RowsIdAmended { get; set; } = new List<string>();
}