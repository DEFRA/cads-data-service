using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtWgSuperAssignment2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal WsaId { get; set; }

    public decimal? WsaWgpIdCurrent { get; set; }

    public decimal? WsaWgpIdAssigned { get; set; }

    public decimal? WsaRouId { get; set; }

    public string? WsaCurrentUser { get; set; }

    public string? WsaCurrentStatus { get; set; }

    public DateOnly? WsaCurrentModifiedDate { get; set; }

    public decimal? WsaCurrentPid { get; set; }

    public decimal? WsaVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtWgSuperAssignment1? AuditTrans { get; set; }
}
