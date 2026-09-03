using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEreportProcessMessage2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public string? ErqFileType { get; set; }

    public decimal? ErqSleepPeriod { get; set; }

    public decimal? ErqDelayPeriod { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtEreportProcessMessage1? AuditTrans { get; set; }
}