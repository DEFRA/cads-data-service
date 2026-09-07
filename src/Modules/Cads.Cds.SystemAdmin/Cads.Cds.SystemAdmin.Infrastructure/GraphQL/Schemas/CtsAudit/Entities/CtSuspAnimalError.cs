using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsAudit.Entities;

public partial class CtSuspAnimalError
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal SaeId { get; set; }

    public decimal? SaeSanId { get; set; }

    public string? SaeErrorCode { get; set; }

    public string? SaeAttributeName { get; set; }

    public DateOnly? SaeCurrentModifiedDate { get; set; }

    public string? SaeCurrentUser { get; set; }

    public string? SaeCurrentStatus { get; set; }

    public decimal? SaeCurrentPid { get; set; }

    public decimal? SaeVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
