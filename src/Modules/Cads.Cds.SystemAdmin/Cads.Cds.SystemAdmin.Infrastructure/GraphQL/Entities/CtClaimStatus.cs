using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtClaimStatus
{
    public decimal ClsId { get; set; }

    public decimal? ClsCurrentPid { get; set; }

    public string? ClsCurrentStatus { get; set; }

    public string? ClsCurrentUser { get; set; }

    public DateOnly? ClsCurrentModifiedDate { get; set; }

    public string? ClsClaimStatus { get; set; }

    public string? ClsDescription { get; set; }

    public decimal? ClsVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtAnimalClaim> CtAnimalClaims { get; set; } = new List<CtAnimalClaim>();
}
