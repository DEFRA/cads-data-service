using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtWorkgroup2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal WgpId { get; set; }

    public string? WgpWorkgroup { get; set; }

    public string? WgpShortName { get; set; }

    public string? WgpLongName { get; set; }

    public string? WgpActiveIndicator { get; set; }

    public string? WgpPrinter { get; set; }

    public string? WgpSummaryType { get; set; }

    public string? WgpReassignLock { get; set; }

    public string? WgpCurrentStatus { get; set; }

    public DateOnly? WgpCurrentModifiedDate { get; set; }

    public string? WgpCurrentUser { get; set; }

    public decimal? WgpCurrentPid { get; set; }

    public decimal? WgpVersion { get; set; }

    public decimal FakeData { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtWorkgroup1? AuditTrans { get; set; }
}