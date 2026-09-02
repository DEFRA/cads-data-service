using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocationPartyRel2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public DateTime AuditedAt { get; set; }

    public decimal LprId { get; set; }

    public decimal? LprLocId { get; set; }

    public decimal? LprLptId { get; set; }

    public decimal? LprParId { get; set; }

    public DateOnly? LprEffectiveFromDate { get; set; }

    public DateOnly? LprEffectiveToDate { get; set; }

    public string? LprCessationReason { get; set; }

    public string? LprComments { get; set; }

    public string? LprCurrentUser { get; set; }

    public DateOnly? LprCurrentModifiedDate { get; set; }

    public string? LprCurrentStatus { get; set; }

    public decimal? LprCurrentPid { get; set; }

    public decimal? LprVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? AuditTransId { get; set; }

    public long? TransId { get; set; }

    public virtual CtLocationPartyRel1? AuditTrans { get; set; }
}
