using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtResetToExtract1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal RteId { get; set; }

    public string? RteTableName { get; set; }

    public string? RteStatus { get; set; }

    public decimal? RteBatch { get; set; }

    public decimal? RowNumber { get; set; }

    public long? RteAudId { get; set; }

    public string? RteAudType { get; set; }

    public DateTime? RteAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtResetToExtract2> CtResetToExtract2s { get; set; } = new List<CtResetToExtract2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}