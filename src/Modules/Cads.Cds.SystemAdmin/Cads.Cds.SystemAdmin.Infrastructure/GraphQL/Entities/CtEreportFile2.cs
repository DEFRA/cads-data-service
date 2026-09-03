using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEreportFile2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal EreId { get; set; }

    public string? EreFileName { get; set; }

    public string? EreFileType { get; set; }

    public decimal? EreLineNumber { get; set; }

    public string? EreRecord { get; set; }

    public DateOnly? EreTimestamp { get; set; }

    public long? TransId { get; set; }

    public virtual CtEreportFile1? AuditTrans { get; set; }
}