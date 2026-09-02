using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtRecdMovementError2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public string? RmeCurrentStatus { get; set; }

    public string? RmeCurrentUser { get; set; }

    public DateOnly? RmeCurrentModifiedDate { get; set; }

    public decimal? RmeCurrentPid { get; set; }

    public decimal? RmeVersion { get; set; }

    public decimal? RmeRmoId { get; set; }

    public string? RmeErrorCode { get; set; }

    public string? RmeAttributeName { get; set; }

    public decimal RmeId { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtRecdMovementError1? AuditTrans { get; set; }
}
