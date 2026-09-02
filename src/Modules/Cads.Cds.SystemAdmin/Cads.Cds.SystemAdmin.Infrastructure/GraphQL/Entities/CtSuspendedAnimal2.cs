using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtSuspendedAnimal2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public DateTime AuditedAt { get; set; }

    public decimal? SanVapId { get; set; }

    public decimal? SanWgpId { get; set; }

    public char? SanApplicationType { get; set; }

    public char? SanCtsIndicator { get; set; }

    public DateOnly? SanApplicReceiptDate { get; set; }

    public DateOnly? SanSuspenseDate { get; set; }

    public string? SanEartag { get; set; }

    public string? SanIntendedAction { get; set; }

    public string? SanPassportVersionNumber { get; set; }

    public string? SanAmendedBy { get; set; }

    public DateOnly? SanAmendedDatetime { get; set; }

    public char? SanSex { get; set; }

    public string? SanBreed { get; set; }

    public DateOnly? SanBirthDate { get; set; }

    public DateOnly? SanPlacementDate { get; set; }

    public decimal? SanLocIdInitial { get; set; }

    public string? SanEartagType { get; set; }

    public string? SanGeneticDamEtType { get; set; }

    public string? SanGeneticDamEartag { get; set; }

    public string? SanSurrDamEtType { get; set; }

    public string? SanSurrDamEartag { get; set; }

    public string? SanSireEtType { get; set; }

    public string? SanSireEartag { get; set; }

    public string? SanElectronicIdentifier { get; set; }

    public string? SanCountryOfOrigin { get; set; }

    public string? SanHealthCertificateNo { get; set; }

    public string? SanImportIdentifier { get; set; }

    public decimal? SanNumberCalfMovts { get; set; }

    public char? SanChrLocationInd { get; set; }

    public char? SanChrCorrectionType { get; set; }

    public DateOnly? SanChangeReceivedDate { get; set; }

    public string? SanAmendReason { get; set; }

    public DateOnly? SanSubmitDatetime { get; set; }

    public decimal? SanLocIdRequest { get; set; }

    public char? SanAmendRetagInd { get; set; }

    public string? SanNewEartagType { get; set; }

    public string? SanNewEartag { get; set; }

    public string? SanSourceType { get; set; }

    public string? SanSourceReference { get; set; }

    public string? SanInterfaceFileName { get; set; }

    public decimal? SanInterfaceFileTxn { get; set; }

    public string? SanOrigIfFileName { get; set; }

    public decimal? SanOrigIfFileTxn { get; set; }

    public DateOnly? SanApplicTargetDate { get; set; }

    public string? SanOriginator { get; set; }

    public decimal? SanVersion { get; set; }

    public string? SanInitialLocationRepd { get; set; }

    public string? SanRequestLocationRepd { get; set; }

    public DateOnly? SanLateAppLetter { get; set; }

    public decimal SanId { get; set; }

    public string? SanCurrentUser { get; set; }

    public string? SanCurrentStatus { get; set; }

    public DateOnly? SanCurrentModifiedDate { get; set; }

    public decimal? SanCurrentPid { get; set; }

    public decimal? SanRanId { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? AuditTransId { get; set; }

    public long? TransId { get; set; }

    public virtual CtSuspendedAnimal1? AuditTrans { get; set; }
}
