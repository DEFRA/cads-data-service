using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtEreportProcessMessage
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public string? ErqFileType { get; set; }

    public decimal? ErqSleepPeriod { get; set; }

    public decimal? ErqDelayPeriod { get; set; }

    public decimal? RowNumber { get; set; }

    public long? ErqAudId { get; set; }

    public string? ErqAudType { get; set; }

    public DateTime? ErqAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }
}