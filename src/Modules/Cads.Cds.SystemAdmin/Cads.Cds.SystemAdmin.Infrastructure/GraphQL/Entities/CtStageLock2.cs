using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtStageLock2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public string? StlFileType { get; set; }

    public string? StlFileName { get; set; }

    public string? StlProcessed { get; set; }

    public DateOnly? StlTimestamp { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtStageLock1? AuditTrans { get; set; }
}