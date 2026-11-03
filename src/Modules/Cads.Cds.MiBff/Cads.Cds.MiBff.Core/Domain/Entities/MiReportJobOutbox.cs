namespace Cads.Cds.MiBff.Core.Domain.Entities;

public class MiReportJobOutbox
{
    public Guid OutboxId { get; set; }
    public Guid ReportJobId { get; set; }
    public string Subject { get; set; } = null!;
    public string Payload { get; set; } = null!;
    public string Status { get; set; } = ReportJobOutboxStatus.Pending;
    public int Attempts { get; set; }
    public string? LastError { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DispatchedAt { get; set; }

    public MiReportJob ReportJob { get; set; } = null!;
}

public static class ReportJobOutboxStatus
{
    public const string Pending = "pending";
    public const string Dispatched = "dispatched";
    public const string Failed = "failed";
}