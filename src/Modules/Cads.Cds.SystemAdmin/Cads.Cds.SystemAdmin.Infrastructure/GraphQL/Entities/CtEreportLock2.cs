using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEreportLock2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public string? ErlFileType { get; set; }

    public string? ErlFileName { get; set; }

    public string? ErlProcessed { get; set; }

    public DateOnly? ErlTimestamp { get; set; }

    public long? TransId { get; set; }

    public virtual CtEreportLock1? AuditTrans { get; set; }
}
