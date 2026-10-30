namespace Cads.Cds.MiBff.Core.Domain.Entities;

public class MiReportJob
{
    public Guid ReportJobId { get; set; }
    public string ReportKey { get; set; } = null!;
    public string ExternalSubject { get; set; } = null!;
    public string RequestPayload { get; set; } = null!;
    public string Status { get; set; } = ReportJobStatus.Pending;
    public string? S3Key { get; set; }
    public int? RowCount { get; set; }
    public string? Error { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? StartedAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public DateTimeOffset? ExpiresAt { get; set; }

    public ICollection<MiReportJobOutbox> OutboxMessages { get; set; } = new List<MiReportJobOutbox>();
}

public static class ReportJobStatus
{
    public const string Pending = "pending";
    public const string Processing = "processing";
    public const string Complete = "complete";
    public const string Failed = "failed";
    public const string Expired = "expired";
}