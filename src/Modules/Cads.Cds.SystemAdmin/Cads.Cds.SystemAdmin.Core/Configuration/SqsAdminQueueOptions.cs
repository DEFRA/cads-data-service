namespace Cads.Cds.SystemAdmin.Core.Configuration;

public class SqsAdminQueueOptions
{
    public required string QueueUrl { get; set; }
    public string? DlqQueueUrl { get; set; }
    public int DefaultReplayBatchSize { get; set; } = 10;
}