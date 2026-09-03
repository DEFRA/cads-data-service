using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtRegisteredAnimal2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public DateTime AuditedAt { get; set; }

    public decimal RanId { get; set; }

    public string? RanCurrentUser { get; set; }

    public string? RanCurrentStatus { get; set; }

    public DateOnly? RanCurrentModifiedDate { get; set; }

    public decimal? RanCurrentPid { get; set; }

    public string? RanCurrentIntendedAction { get; set; }

    public DateOnly? RanCurrentChangeRcvdDate { get; set; }

    public decimal? RanCurrentTracedMoves { get; set; }

    public decimal? RanCurrentAddMoves { get; set; }

    public string? RanCtsIndicator { get; set; }

    public string? RanPassportOrLicence { get; set; }

    public string? RanSex { get; set; }

    public DateOnly? RanBirthDate { get; set; }

    public decimal? RanApplicLine { get; set; }

    public decimal? RanBrdId { get; set; }

    public decimal? RanLocIdPassport { get; set; }

    public decimal? RanVapId { get; set; }

    public decimal? RanMovIdRegistration { get; set; }

    public string? RanPassportModFlag { get; set; }

    public string? RanPassportVersionNumber { get; set; }

    public decimal? RanVersion { get; set; }

    public decimal? RanMovIdDeath { get; set; }

    public decimal? RanCryIdChrOrigin { get; set; }

    public string? RanPassportLocationRepd { get; set; }

    public decimal FakeData { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? AuditTransId { get; set; }

    public long? TransId { get; set; }

    public virtual CtRegisteredAnimal1? AuditTrans { get; set; }
}