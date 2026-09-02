using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtProbityCheck
{
    public decimal PchId { get; set; }

    public string? PchLongDescription { get; set; }

    public string? PchShortDescription { get; set; }

    public DateOnly? PchCheckedToDate { get; set; }

    public decimal? PchCheckPeriod { get; set; }

    public DateOnly? PchNextCheckDate { get; set; }

    public string? PchCurrentUser { get; set; }

    public string? PchCurrentStatus { get; set; }

    public DateOnly? PchCurrentModifiedDate { get; set; }

    public decimal? PchCurrentPid { get; set; }

    public decimal? PchVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtCondition> CtConditions { get; set; } = new List<CtCondition>();
}
