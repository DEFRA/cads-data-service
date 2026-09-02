using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocationIdentifier2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public DateTime AuditedAt { get; set; }

    public decimal LidId { get; set; }

    public decimal? LidLocId { get; set; }

    public DateOnly? LidEffectiveFromDate { get; set; }

    public string? LidIdentifier { get; set; }

    public string? LidFullIdentifier { get; set; }

    public string? LidSubIdentifier { get; set; }

    public DateOnly? LidEffectiveToDate { get; set; }

    public string? LidCurrentStatus { get; set; }

    public DateOnly? LidCurrentModifiedDate { get; set; }

    public string? LidCurrentUser { get; set; }

    public decimal? LidCurrentPid { get; set; }

    public string? LidCurrentAmendReason { get; set; }

    public decimal? LidVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? AuditTransId { get; set; }

    public long? TransId { get; set; }

    public virtual CtLocationIdentifier1? AuditTrans { get; set; }
}
