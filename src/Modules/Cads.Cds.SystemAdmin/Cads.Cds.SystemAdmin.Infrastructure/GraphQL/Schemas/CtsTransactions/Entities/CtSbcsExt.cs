using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtSbcsExt
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
}