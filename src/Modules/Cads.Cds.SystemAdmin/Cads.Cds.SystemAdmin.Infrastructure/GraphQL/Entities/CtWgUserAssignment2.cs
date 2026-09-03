using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtWgUserAssignment2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal WuaId { get; set; }

    public decimal? WuaCusId { get; set; }

    public decimal? WuaWgpId { get; set; }

    public string? WuaWgContactInd { get; set; }

    public string? WuaFavouredWgInd { get; set; }

    public string? WuaCurrentUser { get; set; }

    public string? WuaCurrentStatus { get; set; }

    public DateOnly? WuaCurrentModifiedDate { get; set; }

    public decimal? WuaCurrentPid { get; set; }

    public decimal? WuaVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtWgUserAssignment1? AuditTrans { get; set; }
}