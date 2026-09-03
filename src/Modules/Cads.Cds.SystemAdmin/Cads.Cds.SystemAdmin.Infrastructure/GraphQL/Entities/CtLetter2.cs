using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLetter2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal LetId { get; set; }

    public string? LetType { get; set; }

    public string? LetDescription { get; set; }

    public decimal? LetWgpId { get; set; }

    public string? LetProgramName { get; set; }

    public decimal? LetWgpIdSent { get; set; }

    public string? LetCurrentUser { get; set; }

    public string? LetCurrentStatus { get; set; }

    public DateOnly? LetCurrentModifiedDate { get; set; }

    public decimal? LetCurrentPid { get; set; }

    public decimal? LetVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtLetter1? AuditTrans { get; set; }
}