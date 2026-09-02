using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAnimalClaim2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal AncId { get; set; }

    public decimal? AncRanId { get; set; }

    public decimal? AncClaimSequence { get; set; }

    public DateOnly? AncCurrentModifiedDate { get; set; }

    public decimal? AncCurrentPid { get; set; }

    public string? AncCurrentUser { get; set; }

    public decimal? AncClsId { get; set; }

    public decimal? AncCltId { get; set; }

    public string? AncClaimReference { get; set; }

    public DateOnly? AncRetentionStartDate { get; set; }

    public DateOnly? AncRetentionEndDate { get; set; }

    public string? AncOffice { get; set; }

    public decimal? AncSchemeYear { get; set; }

    public DateOnly? AncSchemeModifiedDatetime { get; set; }

    public decimal? AncVersion { get; set; }

    public string? AncCurrentStatus { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtAnimalClaim1? AuditTrans { get; set; }
}
