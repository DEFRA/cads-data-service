using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtClaMiniDetail1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal CldId { get; set; }

    public decimal? CldCleId { get; set; }

    public decimal? CldBatchId { get; set; }

    public string? CldTableName { get; set; }

    public DateOnly? CldRunStart { get; set; }

    public DateOnly? CldRunEnd { get; set; }

    public DateOnly? CldCurrentModifiedDate { get; set; }

    public long? CldAudId { get; set; }

    public string? CldAudType { get; set; }

    public DateTime? CldAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtClaMiniDetail2> CtClaMiniDetail2s { get; set; } = new List<CtClaMiniDetail2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
