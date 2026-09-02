using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtClaimType
{
    public decimal CltId { get; set; }

    public decimal? CltCurrentPid { get; set; }

    public string? CltCurrentStatus { get; set; }

    public string? CltCurrentUser { get; set; }

    public DateOnly? CltCurrentModifiedDate { get; set; }

    public decimal? CltSchId { get; set; }

    public string? CltClaimType { get; set; }

    public string? CltDescription { get; set; }

    public decimal? CltVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtScheme? CltSch { get; set; }

    public virtual ICollection<CtAnimalClaim> CtAnimalClaims { get; set; } = new List<CtAnimalClaim>();
}
