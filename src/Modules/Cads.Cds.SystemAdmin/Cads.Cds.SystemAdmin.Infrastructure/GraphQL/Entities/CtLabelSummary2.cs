using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLabelSummary2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal LasId { get; set; }

    public decimal? LasLocIdIdentifying { get; set; }

    public decimal? LasLocIdLabels { get; set; }

    public decimal? LasLabelVersionNumber { get; set; }

    public DateOnly? LasLastSubmittedDate { get; set; }

    public string? LasDefaultLabelType { get; set; }

    public decimal? LasDefaultSheetQuantity { get; set; }

    public string? LasCurrentUser { get; set; }

    public string? LasCurrentStatus { get; set; }

    public DateOnly? LasCurrentModifiedDate { get; set; }

    public decimal? LasCurrentPid { get; set; }

    public decimal? LasVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtLabelSummary1? AuditTrans { get; set; }
}