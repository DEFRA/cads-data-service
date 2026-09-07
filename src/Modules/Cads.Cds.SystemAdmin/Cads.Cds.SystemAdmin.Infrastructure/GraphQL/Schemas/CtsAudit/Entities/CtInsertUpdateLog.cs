using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsAudit.Entities;

public partial class CtInsertUpdateLog
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal IulId { get; set; }

    public string? IulSystem { get; set; }

    public string? IulTableName { get; set; }

    public string? IulRecordKey { get; set; }

    public string? IulName { get; set; }

    public DateOnly? IulDateProcessed { get; set; }

    public DateOnly? IulDateProcessedMis { get; set; }

    public string? IulInsertDeleteFlag { get; set; }

    public string? IulCurrentUser { get; set; }

    public string? IulCurrentStatus { get; set; }

    public DateOnly? IulCurrentModifiedDate { get; set; }

    public decimal? IulCurrentPid { get; set; }

    public decimal? IulVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}