using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtWorkgroup
{
    public decimal WgpId { get; set; }

    public string? WgpWorkgroup { get; set; }

    public string? WgpShortName { get; set; }

    public string? WgpLongName { get; set; }

    public char? WgpActiveIndicator { get; set; }

    public string? WgpPrinter { get; set; }

    public string? WgpSummaryType { get; set; }

    public char? WgpReassignLock { get; set; }

    public string? WgpCurrentStatus { get; set; }

    public DateOnly? WgpCurrentModifiedDate { get; set; }

    public string? WgpCurrentUser { get; set; }

    public decimal? WgpCurrentPid { get; set; }

    public decimal? WgpVersion { get; set; }

    public decimal FakeData { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtLetter> CtLetterLetWgpIdSentNavigations { get; set; } = new List<CtLetter>();

    public virtual ICollection<CtLetter> CtLetterLetWgps { get; set; } = new List<CtLetter>();

    public virtual ICollection<CtReceivedApplication> CtReceivedApplications { get; set; } = new List<CtReceivedApplication>();

    public virtual ICollection<CtSuspendedAnimal> CtSuspendedAnimals { get; set; } = new List<CtSuspendedAnimal>();

    public virtual ICollection<CtWgAutoallocation> CtWgAutoallocations { get; set; } = new List<CtWgAutoallocation>();

    public virtual ICollection<CtWgSuperAssignment> CtWgSuperAssignmentWsaWgpIdAssignedNavigations { get; set; } = new List<CtWgSuperAssignment>();

    public virtual ICollection<CtWgSuperAssignment> CtWgSuperAssignmentWsaWgpIdCurrentNavigations { get; set; } = new List<CtWgSuperAssignment>();

    public virtual ICollection<CtWgUserAssignment> CtWgUserAssignments { get; set; } = new List<CtWgUserAssignment>();
}
