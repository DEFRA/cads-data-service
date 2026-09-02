using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtSuspMovementError1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal? SmeSmoId { get; set; }

    public decimal SmeId { get; set; }

    public string? SmeAttributeName { get; set; }

    public string? SmeErrorCode { get; set; }

    public string? SmeCurrentUser { get; set; }

    public DateOnly? SmeCurrentModifiedDate { get; set; }

    public string? SmeCurrentStatus { get; set; }

    public decimal? SmeCurrentPid { get; set; }

    public decimal? SmeVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? SmeAudId { get; set; }

    public string? SmeAudType { get; set; }

    public DateTime? SmeAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtSuspMovementError2> CtSuspMovementError2s { get; set; } = new List<CtSuspMovementError2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
