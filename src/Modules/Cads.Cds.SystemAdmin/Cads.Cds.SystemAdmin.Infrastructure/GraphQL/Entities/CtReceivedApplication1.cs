using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtReceivedApplication1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal RapId { get; set; }

    public string? RapCurrentUser { get; set; }

    public string? RapCurrentStatus { get; set; }

    public DateOnly? RapCurrentModifiedDate { get; set; }

    public decimal? RapCurrentPid { get; set; }

    public string? RapApplicationType { get; set; }

    public string? RapApplicReceiptDate { get; set; }

    public DateOnly? RapApplicTargetDate { get; set; }

    public string? RapCtsIndicator { get; set; }

    public string? RapEartagType { get; set; }

    public string? RapEartag { get; set; }

    public string? RapSourceType { get; set; }

    public string? RapSourceReference { get; set; }

    public string? RapRequestLocType { get; set; }

    public string? RapRequestLocIdentifier { get; set; }

    public string? RapRequestSublocIdentifier { get; set; }

    public string? RapGeneticDamEtType { get; set; }

    public string? RapGeneticDamEartag { get; set; }

    public string? RapSurrDamEtType { get; set; }

    public string? RapSurrDamEartag { get; set; }

    public string? RapSireEtType { get; set; }

    public string? RapSireEartag { get; set; }

    public string? RapBirthDate { get; set; }

    public string? RapPlacementDate { get; set; }

    public string? RapBreed { get; set; }

    public string? RapSex { get; set; }

    public string? RapInitialLocType { get; set; }

    public string? RapInitialLocIdentifier { get; set; }

    public string? RapInitialSublocIdentifier { get; set; }

    public string? RapCountryOfOrigin { get; set; }

    public string? RapHealthCertificateNo { get; set; }

    public string? RapImportIdentifier { get; set; }

    public string? RapElectronicIdentifier { get; set; }

    public string? RapNewEartagType { get; set; }

    public string? RapNewEartag { get; set; }

    public decimal? RapNumberCalfMovts { get; set; }

    public decimal? RapWgpId { get; set; }

    public string? RapInterfaceFileName { get; set; }

    public decimal? RapInterfaceFileTxn { get; set; }

    public string? RapOrigIfFileName { get; set; }

    public decimal? RapOrigIfFileTxn { get; set; }

    public string? RapChrCorrectionType { get; set; }

    public string? RapChrLocationInd { get; set; }

    public DateOnly? RapCreatedDate { get; set; }

    public string? RapIntendedAction { get; set; }

    public string? RapAmendedBy { get; set; }

    public DateOnly? RapAmendedDatetime { get; set; }

    public DateOnly? RapSubmitDatetime { get; set; }

    public string? RapOriginator { get; set; }

    public decimal? RapRanIdReserved { get; set; }

    public decimal? RapVersion { get; set; }

    public DateOnly? RapRequestLetter { get; set; }

    public DateOnly? RapReminderLetter { get; set; }

    public DateOnly? RapRefusedLetter { get; set; }

    public decimal? RowNumber { get; set; }

    public long? RapAudId { get; set; }

    public string? RapAudType { get; set; }

    public DateTime? RapAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtReceivedApplication2> CtReceivedApplication2s { get; set; } = new List<CtReceivedApplication2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}