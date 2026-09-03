using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtConditionMarkerError2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal CmeId { get; set; }

    public decimal? CmeScmId { get; set; }

    public string? CmeAttributeName { get; set; }

    public string? CmeErrorCode { get; set; }

    public string? CmeCurrentStatus { get; set; }

    public string? CmeCurrentUser { get; set; }

    public DateOnly? CmeCurrentModifiedDate { get; set; }

    public decimal? CmeCurrentPid { get; set; }

    public decimal? CmeVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtConditionMarkerError1? AuditTrans { get; set; }
}