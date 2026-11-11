using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cts.Entities;

public partial class CtWgUserAssignment
{
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

    public string TransType { get; set; } = null!;

    public virtual CtCtsUser? WuaCus { get; set; }

    public virtual CtWorkgroup? WuaWgp { get; set; }
}