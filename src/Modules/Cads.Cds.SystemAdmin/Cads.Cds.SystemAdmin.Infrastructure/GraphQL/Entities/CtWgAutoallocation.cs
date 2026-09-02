using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtWgAutoallocation
{
    public decimal WgaId { get; set; }

    public decimal? WgaRouId { get; set; }

    public decimal? WgaWgpId { get; set; }

    public string? WgaAllocation { get; set; }

    public string? WgaAssignment { get; set; }

    public string? WgaCurrentUser { get; set; }

    public decimal? WgaCurrentPid { get; set; }

    public string? WgaCurrentStatus { get; set; }

    public DateOnly? WgaCurrentModifiedDate { get; set; }

    public decimal? WgaVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtAllocRoutine? WgaRou { get; set; }

    public virtual CtWorkgroup? WgaWgp { get; set; }
}
