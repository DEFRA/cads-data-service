using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtMovtCorrSummError2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal MseId { get; set; }

    public decimal? MseMcsId { get; set; }

    public string? MseCurrentUser { get; set; }

    public string? MseCurrentStatus { get; set; }

    public DateOnly? MseCurrentModifiedDate { get; set; }

    public decimal? MseCurrentPid { get; set; }

    public string? MseAttributeName { get; set; }

    public string? MseErrorCode { get; set; }

    public decimal? MseVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtMovtCorrSummError1? AuditTrans { get; set; }
}