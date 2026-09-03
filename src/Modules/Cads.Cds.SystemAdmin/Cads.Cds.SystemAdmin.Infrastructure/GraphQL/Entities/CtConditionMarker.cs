using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtConditionMarker
{
    public decimal ComId { get; set; }

    public decimal? ComRanId { get; set; }

    public decimal? ComCmaId { get; set; }

    public decimal? ComCacId { get; set; }

    public DateOnly? ComEffectiveFromDate { get; set; }

    public string? ComMarkerType { get; set; }

    public string? ComAmendmentReasonCode { get; set; }

    public decimal? ComLastUsedBudNumber { get; set; }

    public decimal? ComAutotagWaveNumber { get; set; }

    public string? ComComments { get; set; }

    public string? ComAmendmentReasonText { get; set; }

    public DateOnly? ComEffectiveToDate { get; set; }

    public decimal? ComLocId { get; set; }

    public decimal? ComCovId { get; set; }

    public string? ComGroupingReference { get; set; }

    public decimal? ComBranchNumber { get; set; }

    public decimal? ComMovId { get; set; }

    public string? ComDocumentRefs { get; set; }

    public DateOnly? ComLastProbityDate { get; set; }

    public string? ComSource { get; set; }

    public decimal? ComCurrentPid { get; set; }

    public string? ComCurrentStatus { get; set; }

    public string? ComCurrentUser { get; set; }

    public DateOnly? ComCurrentModifiedDate { get; set; }

    public decimal? ComVersion { get; set; }

    public decimal FakeData { get; set; }

    public decimal? RowNumber { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? TransId { get; set; }

    public virtual CtConditionActivity? ComCac { get; set; }

    public virtual CtCmAuthority? ComCma { get; set; }

    public virtual CtConditionVariant? ComCov { get; set; }

    public virtual CtLocation? ComLoc { get; set; }

    public virtual CtRegisteredMovement? ComMov { get; set; }

    public virtual CtRegisteredAnimal? ComRan { get; set; }

    public virtual ICollection<CtCmMeasuresResult> CtCmMeasuresResults { get; set; } = new List<CtCmMeasuresResult>();
}