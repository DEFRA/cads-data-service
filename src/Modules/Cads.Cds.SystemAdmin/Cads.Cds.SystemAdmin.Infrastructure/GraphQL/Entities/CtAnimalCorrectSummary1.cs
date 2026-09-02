using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtAnimalCorrectSummary1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

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

    public char? AcsChrCorrectionType { get; set; }

    public char? AcsChrLocationInd { get; set; }

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

    public long? AcsAudId { get; set; }

    public string? AcsAudType { get; set; }

    public DateTime? AcsAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtAnimalCorrectSummary2> CtAnimalCorrectSummary2s { get; set; } = new List<CtAnimalCorrectSummary2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
