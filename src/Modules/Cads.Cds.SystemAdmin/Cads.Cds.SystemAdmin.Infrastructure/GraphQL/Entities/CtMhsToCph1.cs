using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtMhsToCph1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public string? Cph { get; set; }

    public decimal? MhsNumber { get; set; }

    public decimal? RowNumber { get; set; }

    public long? CphAudId { get; set; }

    public string? CphAudType { get; set; }

    public DateTime? CphAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtMhsToCph2> CtMhsToCph2s { get; set; } = new List<CtMhsToCph2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}