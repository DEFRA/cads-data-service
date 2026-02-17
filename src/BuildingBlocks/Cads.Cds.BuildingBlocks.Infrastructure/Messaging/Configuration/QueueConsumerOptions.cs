namespace Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Configuration;

public class QueueConsumerOptions
{
    public required string Name { get; set; }
    public required string QueueUrl { get; set; }
    public string? DlqQueueUrl { get; set; }
    public int MaxNumberOfMessages { get; set; }
    public int WaitTimeSeconds { get; set; }
    public bool HealthcheckEnabled { get; set; }
}