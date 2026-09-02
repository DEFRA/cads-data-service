using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAnimalClaim
{
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

    public virtual CtClaimStatus? AncCls { get; set; }

    public virtual CtClaimType? AncClt { get; set; }

    public virtual CtRegisteredAnimal? AncRan { get; set; }
}
