using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtStageLock1
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

    public virtual ICollection<CtStageLock2> CtStageLock2s { get; set; } = new List<CtStageLock2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}