using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtRegisteredMovement
{
    public decimal MovId { get; set; }

    public string? MovCurrentUser { get; set; }

    public string? MovCurrentStatus { get; set; }

    public DateOnly? MovCurrentModifiedDate { get; set; }

    public decimal? MovCurrentPid { get; set; }

    public decimal? MovRanId { get; set; }

    public decimal? MovLocId { get; set; }

    public string? MovMovementType { get; set; }

    public string? MovDirection { get; set; }

    public DateOnly? MovMovementDate { get; set; }

    public DateOnly? MovMovementReceivedDate { get; set; }

    public DateOnly? MovVersionCreationDate { get; set; }

    public string? MovReportedEartag { get; set; }

    public string? MovSourceType { get; set; }

    public decimal? MovOriginator { get; set; }

    public string? MovOriginatorsReference { get; set; }

    public string? MovKillNumber { get; set; }

    public string? MovEidReported { get; set; }

    public decimal? MovCryIdImport { get; set; }

    public string? MovHealthCertificateNo { get; set; }

    public string? MovInterfaceFileName { get; set; }

    public decimal? MovInterfaceFileTxn { get; set; }

    public string? MovOrigInterfaceFileName { get; set; }

    public decimal? MovOrigInterfaceFileTxn { get; set; }

    public string? MovAmendmentReason { get; set; }

    public string? MovAmendedBy { get; set; }

    public DateOnly? MovSuspenseDate { get; set; }

    public DateOnly? MovProbityReportDate { get; set; }

    public DateOnly? MovAnomalyCheckDate { get; set; }

    public string? MovAnomalyCode { get; set; }

    public string? MovInferMovementRule { get; set; }

    public decimal? MovVersion { get; set; }

    public string? MovLocationRepd { get; set; }

    public decimal FakeData { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtAnimalChange> CtAnimalChanges { get; set; } = new List<CtAnimalChange>();

    public virtual ICollection<CtConditionMarker> CtConditionMarkers { get; set; } = new List<CtConditionMarker>();

    public virtual ICollection<CtMovtCorrectSummary> CtMovtCorrectSummaries { get; set; } = new List<CtMovtCorrectSummary>();

    public virtual CtCountry? MovCryIdImportNavigation { get; set; }

    public virtual CtLocation? MovLoc { get; set; }

    public virtual CtRegisteredAnimal? MovRan { get; set; }
}
