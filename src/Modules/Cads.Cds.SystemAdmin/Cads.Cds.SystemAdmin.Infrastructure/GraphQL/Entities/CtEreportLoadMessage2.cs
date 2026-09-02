using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEreportLoadMessage2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public string? ErmDirectoryKey { get; set; }

    public string? ErmFileType { get; set; }

    public string? ErmFilePrefix { get; set; }

    public string? ErmFileSuffix { get; set; }

    public decimal? ErmSleepPeriod { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtEreportLoadMessage1? AuditTrans { get; set; }
}
