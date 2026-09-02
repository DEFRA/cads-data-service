using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEreportLoadMessage1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public string? ErmDirectoryKey { get; set; }

    public string? ErmFileType { get; set; }

    public string? ErmFilePrefix { get; set; }

    public string? ErmFileSuffix { get; set; }

    public decimal? ErmSleepPeriod { get; set; }

    public decimal? RowNumber { get; set; }

    public long? ErmAudId { get; set; }

    public string? ErmAudType { get; set; }

    public DateTime? ErmAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtEreportLoadMessage2> CtEreportLoadMessage2s { get; set; } = new List<CtEreportLoadMessage2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
