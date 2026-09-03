using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAnimalChange2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal AchId { get; set; }

    public string? AchCurrentStatus { get; set; }

    public string? AchCurrentUser { get; set; }

    public DateOnly? AchCurrentModifiedDate { get; set; }

    public decimal? AchCurrentPid { get; set; }

    public decimal? AchRanIdDocIssued { get; set; }

    public decimal? AchLocIdDocIssued { get; set; }

    public DateOnly? AchDocIssuedDate { get; set; }

    public string? AchPassportVersionNumber { get; set; }

    public decimal? AchMovIdDeathCancel { get; set; }

    public string? AchBreedOriginal { get; set; }

    public string? AchBreedNew { get; set; }

    public string? AchSexOriginal { get; set; }

    public string? AchSexNew { get; set; }

    public DateOnly? AchBirthDateOriginal { get; set; }

    public DateOnly? AchBirthDateNew { get; set; }

    public string? AchEartagOriginal { get; set; }

    public string? AchEartagNew { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtAnimalChange1? AuditTrans { get; set; }
}