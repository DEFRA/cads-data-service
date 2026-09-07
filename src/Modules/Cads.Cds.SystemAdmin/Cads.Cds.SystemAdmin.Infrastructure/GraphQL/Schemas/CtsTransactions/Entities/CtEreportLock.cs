using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtEreportLock
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public string? ErlFileType { get; set; }

    public string? ErlFileName { get; set; }

    public string? ErlProcessed { get; set; }

    public DateOnly? ErlTimestamp { get; set; }

    public long? ErlAudId { get; set; }

    public string? ErlAudType { get; set; }

    public DateTime? ErlAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }
}
