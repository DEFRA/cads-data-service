using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtConditionActivity
{
    public decimal CacId { get; set; }

    public decimal? CacConId { get; set; }

    public string? CacShortDescription { get; set; }

    public string? CacActivityCode { get; set; }

    public string? CacLongDescription { get; set; }

    public string? CacCurrentUser { get; set; }

    public string? CacCurrentStatus { get; set; }

    public decimal? CacCurrentPid { get; set; }

    public DateOnly? CacCurrentModifiedDate { get; set; }

    public decimal? CacVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual CtCondition? CacCon { get; set; }

    public virtual ICollection<CtConditionMarker> CtConditionMarkers { get; set; } = new List<CtConditionMarker>();
}
