using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtConditionType
{
    public decimal CotId { get; set; }

    public string? CotConditionType { get; set; }

    public string? CotShortDescription { get; set; }

    public DateOnly? CotEffectiveFromDate { get; set; }

    public string? CotLongDescription { get; set; }

    public DateOnly? CotEffectiveToDate { get; set; }

    public string? CotCessationReason { get; set; }

    public string? CotAccessGroup { get; set; }

    public string? CotCurrentUser { get; set; }

    public string? CotCurrentStatus { get; set; }

    public DateOnly? CotCurrentModifiedDate { get; set; }

    public decimal? CotCurrentPid { get; set; }

    public decimal? CotVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtCmAuthority> CtCmAuthorities { get; set; } = new List<CtCmAuthority>();

    public virtual ICollection<CtCondition> CtConditions { get; set; } = new List<CtCondition>();
}