using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtStageLock
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public string? StlFileType { get; set; }

    public string? StlFileName { get; set; }

    public string? StlProcessed { get; set; }

    public DateOnly? StlTimestamp { get; set; }

    public decimal? RowNumber { get; set; }

    public long? StlAudId { get; set; }

    public string? StlAudType { get; set; }

    public DateTime? StlAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }
}