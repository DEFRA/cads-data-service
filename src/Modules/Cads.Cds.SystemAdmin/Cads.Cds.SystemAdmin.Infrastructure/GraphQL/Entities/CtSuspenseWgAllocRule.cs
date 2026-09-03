using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtSuspenseWgAllocRule
{
    public decimal SwaId { get; set; }

    public decimal? SwaRouId { get; set; }

    public decimal? SwaPriority { get; set; }

    public string? SwaRule { get; set; }

    public DateOnly? SwaReportedBadDate { get; set; }

    public string? SwaRuleFormula { get; set; }

    public string? SwaCurrentUser { get; set; }

    public string? SwaCurrentStatus { get; set; }

    public DateOnly? SwaCurrentModifiedDate { get; set; }

    public decimal? SwaCurrentPid { get; set; }

    public decimal? SwaVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtAllocRoutine? SwaRou { get; set; }
}