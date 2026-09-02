using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtCtsUser2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

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

    public virtual CtCtsUser1? AuditTrans { get; set; }
}
