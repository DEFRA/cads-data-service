using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAnimalCorrSummError2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal AseId { get; set; }

    public decimal? AseAcsId { get; set; }

    public string? AseCurrentUser { get; set; }

    public string? AseCurrentStatus { get; set; }

    public DateOnly? AseCurrentModifiedDate { get; set; }

    public decimal? AseCurrentPid { get; set; }

    public string? AseAttributeName { get; set; }

    public string? AseErrorCode { get; set; }

    public decimal? AseVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtAnimalCorrSummError1? AuditTrans { get; set; }
}