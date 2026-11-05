namespace Cads.Cds.MiBff.Core.Configuration;

public class ReportJobOutboxDispatcherOptions
{
    /// <summary>
    /// How often the dispatcher polls for pending outbox rows.
    /// </summary>
    public int PollIntervalSeconds { get; set; } = 5;

    /// <summary>
    /// Maximum number of outbox rows claimed per poll.
    /// </summary>
    public int BatchSize { get; set; } = 20;

    /// <summary>
    /// Number of failed publish attempts before an outbox row is marked 'failed'
    /// instead of being requeued as 'pending'.
    /// </summary>
    public int MaxAttempts { get; set; } = 5;
}

