using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtSuspenseCharAllocRule
{
    public decimal ScaId { get; set; }

    public string? ScaSuspenseChar { get; set; }

    public decimal? ScaRouId { get; set; }

    public string? ScaSubroutine { get; set; }

    public string? ScaTestValue { get; set; }

    public string? ScaCurrentUser { get; set; }

    public string? ScaCurrentStatus { get; set; }

    public DateOnly? ScaCurrentModifiedDate { get; set; }

    public decimal? ScaCurrentPid { get; set; }

    public decimal? ScaVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtAllocRoutine? ScaRou { get; set; }
}