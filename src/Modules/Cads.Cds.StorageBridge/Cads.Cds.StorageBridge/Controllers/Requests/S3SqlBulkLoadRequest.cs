namespace Cads.Cds.StorageBridge.Controllers.Requests;

public class S3SqlBulkLoadRequest
{
    public required string SourceKey { get; set; }
}