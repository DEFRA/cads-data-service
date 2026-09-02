using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEreportProcessMessage1
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

    public virtual ICollection<CtEreportProcessMessage2> CtEreportProcessMessage2s { get; set; } = new List<CtEreportProcessMessage2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
