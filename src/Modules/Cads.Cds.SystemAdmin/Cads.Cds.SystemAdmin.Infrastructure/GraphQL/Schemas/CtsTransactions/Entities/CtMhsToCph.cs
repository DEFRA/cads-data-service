using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtMhsToCph
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
}
