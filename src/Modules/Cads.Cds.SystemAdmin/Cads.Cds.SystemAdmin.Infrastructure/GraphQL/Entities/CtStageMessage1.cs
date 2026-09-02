using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtStageMessage1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public string? StmDirectoryKey { get; set; }

    public string? StmFileType { get; set; }

    public string? StmFilePrefix { get; set; }

    public string? StmFileSuffix { get; set; }

    public decimal? StmSleepPeriod { get; set; }

    public decimal? RowNumber { get; set; }

    public long? StmAudId { get; set; }

    public string? StmAudType { get; set; }

    public DateTime? StmAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtStageMessage2> CtStageMessage2s { get; set; } = new List<CtStageMessage2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
