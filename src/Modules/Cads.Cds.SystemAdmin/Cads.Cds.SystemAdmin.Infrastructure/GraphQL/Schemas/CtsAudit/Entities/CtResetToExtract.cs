using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsAudit.Entities;

public partial class CtResetToExtract
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal RteId { get; set; }

    public string? RteTableName { get; set; }

    public string? RteStatus { get; set; }

    public decimal? RteBatch { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
