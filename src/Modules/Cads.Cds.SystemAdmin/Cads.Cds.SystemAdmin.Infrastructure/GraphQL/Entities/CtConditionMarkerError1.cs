using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtConditionMarkerError1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal CmeId { get; set; }

    public decimal? CmeScmId { get; set; }

    public string? CmeAttributeName { get; set; }

    public string? CmeErrorCode { get; set; }

    public string? CmeCurrentStatus { get; set; }

    public string? CmeCurrentUser { get; set; }

    public DateOnly? CmeCurrentModifiedDate { get; set; }

    public decimal? CmeCurrentPid { get; set; }

    public decimal? CmeVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? CmeAudId { get; set; }

    public string? CmeAudType { get; set; }

    public DateTime? CmeAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtConditionMarkerError2> CtConditionMarkerError2s { get; set; } = new List<CtConditionMarkerError2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
