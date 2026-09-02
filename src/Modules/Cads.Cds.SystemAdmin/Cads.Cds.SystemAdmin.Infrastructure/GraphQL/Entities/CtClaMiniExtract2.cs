using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtClaMiniExtract2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal CleId { get; set; }

    public decimal? CleBatchId { get; set; }

    public DateOnly? CleRunStart { get; set; }

    public DateOnly? CleRunEnd { get; set; }

    public DateOnly? CleDataReadStart { get; set; }

    public DateOnly? CleDataReadEnd { get; set; }

    public string? CleRunStatus { get; set; }

    public DateOnly? CleCurrentModifiedDate { get; set; }

    public string? CleBulkRunStop { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtClaMiniExtract1? AuditTrans { get; set; }
}
