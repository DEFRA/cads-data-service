using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtMgtWgAllocationRule
{
    public decimal WarId { get; set; }

    public decimal? WarRouId { get; set; }

    public decimal? WarPriority { get; set; }

    public string? WarSuspenseType { get; set; }

    public string? WarRule { get; set; }

    public string? WarRuleFormula { get; set; }

    public string? WarCurrentUser { get; set; }

    public string? WarCurrentStatus { get; set; }

    public DateOnly? WarCurrentModifiedDate { get; set; }

    public decimal? WarCurrentPid { get; set; }

    public decimal? WarVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtAllocRoutine? WarRou { get; set; }
}