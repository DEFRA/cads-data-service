using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAnimalIdentifier2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal AidId { get; set; }

    public string? AidIdentifier { get; set; }

    public string? AidIdentifierType { get; set; }

    public DateOnly? AidEffectiveFromDate { get; set; }

    public DateOnly? AidEffectiveToDate { get; set; }

    public decimal? AidLocIdAssigned { get; set; }

    public string? AidCurrentFlag { get; set; }

    public decimal? AidRanId { get; set; }

    public decimal? AidEtgId { get; set; }

    public decimal? AidEidId { get; set; }

    public string? AidCurrentUser { get; set; }

    public string? AidCurrentStatus { get; set; }

    public DateOnly? AidCurrentModifiedDate { get; set; }

    public decimal? AidCurrentPid { get; set; }

    public decimal? AidAidIdOriginal { get; set; }

    public decimal? AidAidIdPrevious { get; set; }

    public decimal? AidVersion { get; set; }

    public string? AidAssignedLocationRepd { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtAnimalIdentifier1? AuditTrans { get; set; }
}