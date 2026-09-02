using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAllocRoutine
{
    public decimal RouId { get; set; }

    public string? RouRoutine { get; set; }

    public string? RouAllocationType { get; set; }

    public string? RouLongDescription { get; set; }

    public string? RouCurrentUser { get; set; }

    public string? RouCurrentStatus { get; set; }

    public DateOnly? RouCurrentModifiedDate { get; set; }

    public decimal? RouCurrentPid { get; set; }

    public decimal? RouVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtMgtWgAllocationRule> CtMgtWgAllocationRules { get; set; } = new List<CtMgtWgAllocationRule>();

    public virtual ICollection<CtSuspenseCharAllocRule> CtSuspenseCharAllocRules { get; set; } = new List<CtSuspenseCharAllocRule>();

    public virtual ICollection<CtSuspenseWgAllocRule> CtSuspenseWgAllocRules { get; set; } = new List<CtSuspenseWgAllocRule>();

    public virtual ICollection<CtWgAutoallocation> CtWgAutoallocations { get; set; } = new List<CtWgAutoallocation>();

    public virtual ICollection<CtWgSuperAssignment> CtWgSuperAssignments { get; set; } = new List<CtWgSuperAssignment>();
}
