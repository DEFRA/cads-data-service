using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Entities;

public partial class MiReportJobOutbox
{
    public Guid OutboxId { get; set; }

    public Guid ReportJobId { get; set; }

    public string Subject { get; set; } = null!;

    public string Payload { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int Attempts { get; set; }

    public string? LastError { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DispatchedAt { get; set; }

    public virtual MiReportJob ReportJob { get; set; } = null!;
}