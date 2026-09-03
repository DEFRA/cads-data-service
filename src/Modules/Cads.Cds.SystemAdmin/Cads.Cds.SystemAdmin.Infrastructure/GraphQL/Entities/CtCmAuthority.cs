using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtCmAuthority
{
    public decimal CmaId { get; set; }

    public decimal? CmaCotId { get; set; }

    public string? CmaAuthorityCode { get; set; }

    public string? CmaShortName { get; set; }

    public string? CmaLongName { get; set; }

    public decimal? CmaCurrentPid { get; set; }

    public string? CmaCurrentStatus { get; set; }

    public DateOnly? CmaCurrentModifiedDate { get; set; }

    public string? CmaCurrentUser { get; set; }

    public decimal? CmaVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual CtConditionType? CmaCot { get; set; }

    public virtual ICollection<CtConditionMarker> CtConditionMarkers { get; set; } = new List<CtConditionMarker>();
}