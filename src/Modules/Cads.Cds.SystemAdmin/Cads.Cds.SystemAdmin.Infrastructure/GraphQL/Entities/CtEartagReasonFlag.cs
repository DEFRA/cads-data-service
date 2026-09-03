using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEartagReasonFlag
{
    public decimal ErfId { get; set; }

    public string? ErfEartagAuthority { get; set; }

    public decimal? ErfEtrId { get; set; }

    public decimal? ErfManualEntryDefaultInd { get; set; }

    public decimal? ErfManualDeletionInd { get; set; }

    public decimal? ErfBatchUpdateAmendFlag { get; set; }

    public decimal? ErfCtsAnimalRegFlag { get; set; }

    public decimal? ErfManualOverride { get; set; }

    public decimal? ErfCtsGenSurrSireAllowed { get; set; }

    public decimal? ErfManualEntryInd { get; set; }

    public decimal? ErfBackcaptureRegnFlag { get; set; }

    public decimal? ErfManualUpdateFlag { get; set; }

    public string? ErfCurrentStatus { get; set; }

    public string? ErfCurrentUser { get; set; }

    public DateOnly? ErfCurrentModifiedDate { get; set; }

    public decimal? ErfCurrentPid { get; set; }

    public decimal? ErfVersion { get; set; }

    public decimal FakeData { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual ICollection<CtEartagStaging> CtEartagStagings { get; set; } = new List<CtEartagStaging>();

    public virtual ICollection<CtEartag> CtEartags { get; set; } = new List<CtEartag>();

    public virtual CtEartagReason? ErfEtr { get; set; }
}