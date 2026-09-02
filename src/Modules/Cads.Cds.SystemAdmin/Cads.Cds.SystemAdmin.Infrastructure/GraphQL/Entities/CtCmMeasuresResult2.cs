using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtCmMeasuresResult2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public DateTime AuditedAt { get; set; }

    public decimal? CmrComId { get; set; }

    public string? CmrResultChar { get; set; }

    public string? CmrMeasureChar { get; set; }

    public decimal? CmrResultNum { get; set; }

    public decimal? CmrMeasureNum { get; set; }

    public string? CmrCurrentUser { get; set; }

    public DateOnly? CmrCurrentModifiedDate { get; set; }

    public string? CmrCurrentStatus { get; set; }

    public decimal? CmrCurrentPid { get; set; }

    public decimal? CmrVersion { get; set; }

    public decimal CmrId { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? AuditTransId { get; set; }

    public long? TransId { get; set; }

    public virtual CtCmMeasuresResult1? AuditTrans { get; set; }
}
