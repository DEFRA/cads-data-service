using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cads.Entities;

public partial class MiReportJob
{
    public Guid ReportJobId { get; set; }

    public string ReportKey { get; set; } = null!;

    public string ExternalSubject { get; set; } = null!;

    public string RequestPayload { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? S3Key { get; set; }

    public int? RowCount { get; set; }

    public string? Error { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public virtual ICollection<MiReportJobOutbox> MiReportJobOutboxes { get; set; } = new List<MiReportJobOutbox>();
}