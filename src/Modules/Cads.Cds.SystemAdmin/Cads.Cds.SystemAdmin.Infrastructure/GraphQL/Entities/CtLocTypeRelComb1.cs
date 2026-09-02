using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocTypeRelComb1
{
    public decimal LrcId { get; set; }

    public decimal? LrcLtyId1 { get; set; }

    public decimal? LrcLtyId2 { get; set; }

    public decimal? LrcLrtId { get; set; }

    public string? LrcCurrentUser { get; set; }

    public DateOnly? LrcCurrentModifiedDate { get; set; }

    public string? LrcCurrentStatus { get; set; }

    public decimal? LrcCurrentPid { get; set; }

    public decimal? LrcVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? LrcAudId { get; set; }

    public string? LrcAudType { get; set; }

    public DateTime? LrcAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
