using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtBatchRetentionConf
{
    public string? BrtItemId { get; set; }

    public decimal? BrtRetentionDays { get; set; }

    public string? BrtDescription { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? BrtAudId { get; set; }

    public string? BrtAudType { get; set; }

    public DateTime? BrtAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }
}