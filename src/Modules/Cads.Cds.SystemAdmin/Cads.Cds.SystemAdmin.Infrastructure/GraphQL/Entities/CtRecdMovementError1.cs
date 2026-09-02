using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtRecdMovementError1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public string? RmeCurrentStatus { get; set; }

    public string? RmeCurrentUser { get; set; }

    public DateOnly? RmeCurrentModifiedDate { get; set; }

    public decimal? RmeCurrentPid { get; set; }

    public decimal? RmeVersion { get; set; }

    public decimal? RmeRmoId { get; set; }

    public string? RmeErrorCode { get; set; }

    public string? RmeAttributeName { get; set; }

    public decimal RmeId { get; set; }

    public decimal? RowNumber { get; set; }

    public long? RmeAudId { get; set; }

    public string? RmeAudType { get; set; }

    public DateTime? RmeAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtRecdMovementError2> CtRecdMovementError2s { get; set; } = new List<CtRecdMovementError2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
