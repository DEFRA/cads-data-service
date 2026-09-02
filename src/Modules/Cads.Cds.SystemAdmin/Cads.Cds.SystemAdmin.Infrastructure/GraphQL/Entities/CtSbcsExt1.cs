using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtSbcsExt1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public string? SxtId { get; set; }

    public decimal? RowNumber { get; set; }

    public long? SxtAudId { get; set; }

    public string? SxtAudType { get; set; }

    public DateTime? SxtAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtSbcsExt2> CtSbcsExt2s { get; set; } = new List<CtSbcsExt2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
