namespace Cads.Cds.SystemAdmin.Core.Configuration;

/// <summary>
/// Describes a queue that is exposed via the SQS admin endpoints, including its
/// optional dead-letter queue mapping used for DLQ replay operations.
/// </summary>
public class SqsAdminQueueOptions
{
    public required string QueueUrl { get; set; }

    public string? DlqQueueUrl { get; set; }

    /// <summary>
    /// Default number of messages moved per batch when replaying from the DLQ, unless
    /// overridden by the caller.
    /// </summary>
    public int DefaultReplayBatchSize { get; set; } = 10;
}

