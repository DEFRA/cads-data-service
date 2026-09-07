using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsAudit.Entities;

public partial class CtRecdApplicationError
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal RaeId { get; set; }

    public decimal? RaeRapId { get; set; }

    public string? RaeAttributeName { get; set; }

    public string? RaeErrorCode { get; set; }

    public string? RaeCurrentStatus { get; set; }

    public string? RaeCurrentUser { get; set; }

    public DateOnly? RaeCurrentModifiedDate { get; set; }

    public decimal? RaeCurrentPid { get; set; }

    public decimal? RaeVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}