using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtParamValue
{
    public decimal PvlId { get; set; }

    public string? PvlParam { get; set; }

    public decimal? PvlPhdId { get; set; }

    public string? PvlParamValue { get; set; }

    public string? PvlParamShortDesc { get; set; }

    public string? PvlParamLongDesc { get; set; }

    public decimal? PvlSequence { get; set; }

    public string? PvlCurrentUser { get; set; }

    public string? PvlCurrentStatus { get; set; }

    public DateOnly? PvlCurrentModifiedDate { get; set; }

    public decimal? PvlCurrentPid { get; set; }

    public decimal? PvlVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtParamValueGroup> CtParamValueGroups { get; set; } = new List<CtParamValueGroup>();

    public virtual CtParamHeader? PvlPhd { get; set; }
}
