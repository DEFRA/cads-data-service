using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEartagReasonFlag1
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

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? ErfAudId { get; set; }

    public string? ErfAudType { get; set; }

    public DateTime? ErfAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
