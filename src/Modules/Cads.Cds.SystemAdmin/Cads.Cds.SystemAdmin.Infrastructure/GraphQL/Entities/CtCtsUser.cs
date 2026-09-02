using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtCtsUser
{
    public decimal CusId { get; set; }

    public string? CusUserIdentifier { get; set; }

    public string? CusColonFlag { get; set; }

    public string? CusGrade { get; set; }

    public string? CusTeamReference { get; set; }

    public string? CusAccessGroup { get; set; }

    public string? CusRoomName { get; set; }

    public string? CusEmailAddress { get; set; }

    public decimal? CusVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtWgUserAssignment> CtWgUserAssignments { get; set; } = new List<CtWgUserAssignment>();
}
