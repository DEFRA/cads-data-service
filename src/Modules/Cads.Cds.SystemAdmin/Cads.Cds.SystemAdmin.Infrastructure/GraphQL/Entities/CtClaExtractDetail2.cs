using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtClaExtractDetail2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal CldId { get; set; }

    public decimal? CldCleId { get; set; }

    public decimal? CldBatchId { get; set; }

    public string? CldTableName { get; set; }

    public decimal? CldRecordCount { get; set; }

    public DateOnly? CldRunStart { get; set; }

    public DateOnly? CldRunEnd { get; set; }

    public DateOnly? CldCurrentModifiedDate { get; set; }

    public long? TransId { get; set; }

    public virtual CtClaExtractDetail1? AuditTrans { get; set; }
}
