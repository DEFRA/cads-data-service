using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtWgUserAssignment
{
    public decimal WuaId { get; set; }

    public decimal? WuaCusId { get; set; }

    public decimal? WuaWgpId { get; set; }

    public char? WuaWgContactInd { get; set; }

    public char? WuaFavouredWgInd { get; set; }

    public string? WuaCurrentUser { get; set; }

    public string? WuaCurrentStatus { get; set; }

    public DateOnly? WuaCurrentModifiedDate { get; set; }

    public decimal? WuaCurrentPid { get; set; }

    public decimal? WuaVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtCtsUser? WuaCus { get; set; }

    public virtual CtWorkgroup? WuaWgp { get; set; }
}
