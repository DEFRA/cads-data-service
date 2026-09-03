using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAnimalCorrectSummary2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public string? AcsInitInitialLocIdent { get; set; }

    public string? AcsInitInitialSublocIdent { get; set; }

    public string? AcsInitPlacementDate { get; set; }

    public string? AcsInitPreviousEartag { get; set; }

    public string? AcsInitCountryOfOrigin { get; set; }

    public string? AcsInitHealthCertificateNo { get; set; }

    public string? AcsInitElectronicIdentifier { get; set; }

    public string? AcsInitImportIdentifier { get; set; }

    public decimal? AcsInitNumberCalfMovts { get; set; }

    public string? AcsInitIntendedAction { get; set; }

    public string? AcsSubmitIntendedAction { get; set; }

    public string? AcsSubmitAmendReason { get; set; }

    public string? AcsSubmitStatus { get; set; }

    public string? AcsSubmitUser { get; set; }

    public string? AcsSubmitWorkgroup { get; set; }

    public DateOnly? AcsSubmitDate { get; set; }

    public DateOnly? AcsChangeReceivedDate { get; set; }

    public DateOnly? AcsSuspenseDatetime { get; set; }

    public string? AcsAmendRetagInd { get; set; }

    public string? AcsNewEartagType { get; set; }

    public string? AcsNewEartag { get; set; }

    public string? AcsChrCorrectionType { get; set; }

    public string? AcsChrLocationInd { get; set; }

    public string? AcsInterfaceFileName { get; set; }

    public decimal? AcsInterfaceFileTxn { get; set; }

    public decimal? AcsVersion { get; set; }

    public decimal? AcsMigratedAppsusKey { get; set; }

    public DateOnly? AcsLateAppLetter { get; set; }

    public DateOnly? AcsRequestLetter { get; set; }

    public DateOnly? AcsReminderLetter { get; set; }

    public DateOnly? AcsRefusedLetter { get; set; }

    public decimal AcsId { get; set; }

    public string? AcsCurrentUser { get; set; }

    public string? AcsCurrentStatus { get; set; }

    public DateOnly? AcsCurrentModifiedDate { get; set; }

    public decimal? AcsCurrentPid { get; set; }

    public string? AcsSanOrRapInd { get; set; }

    public decimal? AcsSanId { get; set; }

    public decimal? AcsRapId { get; set; }

    public decimal? AcsRanId { get; set; }

    public decimal? AcsVapId { get; set; }

    public string? AcsApplicationType { get; set; }

    public string? AcsSourceType { get; set; }

    public string? AcsSourceReference { get; set; }

    public string? AcsCtsIndicator { get; set; }

    public string? AcsPassportVersionNo { get; set; }

    public string? AcsInitApplicReceiptDate { get; set; }

    public DateOnly? AcsInitApplicTargetDate { get; set; }

    public string? AcsInitRequestLocType { get; set; }

    public string? AcsInitRequestLocIdent { get; set; }

    public string? AcsInitRequestSublocIdent { get; set; }

    public string? AcsInitEartagType { get; set; }

    public string? AcsInitEartag { get; set; }

    public string? AcsInitBreed { get; set; }

    public string? AcsInitBirthDate { get; set; }

    public string? AcsInitSex { get; set; }

    public string? AcsInitGeneticDamEtType { get; set; }

    public string? AcsInitGeneticDamEartag { get; set; }

    public string? AcsInitSurrDamEtType { get; set; }

    public string? AcsInitSurrDamEartag { get; set; }

    public string? AcsInitSireEtType { get; set; }

    public string? AcsInitSireEartag { get; set; }

    public string? AcsInitInitialLocType { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtAnimalCorrectSummary1? AuditTrans { get; set; }
}
