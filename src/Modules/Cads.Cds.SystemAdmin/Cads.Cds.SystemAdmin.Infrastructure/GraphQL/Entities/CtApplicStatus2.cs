using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtApplicStatus2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal ApsId { get; set; }

    public decimal? ApsVapId { get; set; }

    public string? ApsUser { get; set; }

    public string? ApsStatus { get; set; }

    public DateOnly? ApsModifiedDate { get; set; }

    public decimal? ApsPid { get; set; }

    public string? ApsIntendedAction { get; set; }

    public decimal? ApsVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtApplicStatus1? AuditTrans { get; set; }
}
