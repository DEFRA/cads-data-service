using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtSublocationType
{
    public decimal SltId { get; set; }

    public string? SltSublocType { get; set; }

    public string? SltShortDescription { get; set; }

    public string? SltLongDescription { get; set; }

    public string? SltPeerLinkPermitted { get; set; }

    public string? SltHierLinkPermitted { get; set; }

    public string? SltMovementSublocInd { get; set; }

    public string? SltUseSublocAddress { get; set; }

    public string? SltCurrentUser { get; set; }

    public string? SltCurrentStatus { get; set; }

    public DateOnly? SltCurrentModifiedDate { get; set; }

    public decimal? SltCurrentPid { get; set; }

    public decimal? SltVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtLocation> CtLocations { get; set; } = new List<CtLocation>();
}