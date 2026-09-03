using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEreportFile1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal EreId { get; set; }

    public string? EreFileName { get; set; }

    public string? EreFileType { get; set; }

    public decimal? EreLineNumber { get; set; }

    public string? EreRecord { get; set; }

    public DateOnly? EreTimestamp { get; set; }

    public long? EreAudId { get; set; }

    public string? EreAudType { get; set; }

    public DateTime? EreAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtEreportFile2> CtEreportFile2s { get; set; } = new List<CtEreportFile2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}