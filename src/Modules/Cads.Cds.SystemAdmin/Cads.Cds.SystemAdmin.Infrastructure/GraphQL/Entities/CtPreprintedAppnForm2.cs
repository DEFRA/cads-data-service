using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtPreprintedAppnForm2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal PafId { get; set; }

    public decimal? PafEtgId { get; set; }

    public decimal? PafPpgId { get; set; }

    public string? PafReasonForIssue { get; set; }

    public decimal? PafInterfaceTxnNumber { get; set; }

    public string? PafInterfaceFilename { get; set; }

    public DateOnly? PafDateIssued { get; set; }

    public string? PafCurrentStatus { get; set; }

    public DateOnly? PafCurrentModifiedDate { get; set; }

    public string? PafCurrentUser { get; set; }

    public decimal? PafCurrentPid { get; set; }

    public decimal? PafVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtPreprintedAppnForm1? AuditTrans { get; set; }
}
