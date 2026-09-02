using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEartagType1
{
    public decimal EttId { get; set; }

    public string? EttEartagType { get; set; }

    public string? EttCrExport { get; set; }

    public string? EttDescription { get; set; }

    public decimal? EttEtfId { get; set; }

    public string? EttShortDescription { get; set; }

    public string? EttCurrentUser { get; set; }

    public string? EttCurrentStatus { get; set; }

    public DateOnly? EttCurrentModifiedDate { get; set; }

    public decimal? EttCurrentPid { get; set; }

    public decimal? EttVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? EttAudId { get; set; }

    public string? EttAudType { get; set; }

    public DateTime? EttAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
