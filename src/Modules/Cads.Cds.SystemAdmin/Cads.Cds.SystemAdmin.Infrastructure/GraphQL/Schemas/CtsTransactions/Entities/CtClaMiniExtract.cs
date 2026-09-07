using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtClaMiniExtract
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal CleId { get; set; }

    public decimal? CleBatchId { get; set; }

    public DateOnly? CleRunStart { get; set; }

    public DateOnly? CleRunEnd { get; set; }

    public DateOnly? CleDataReadStart { get; set; }

    public DateOnly? CleDataReadEnd { get; set; }

    public string? CleRunStatus { get; set; }

    public DateOnly? CleCurrentModifiedDate { get; set; }

    public string? CleBulkRunStop { get; set; }

    public decimal? RowNumber { get; set; }

    public long? CleAudId { get; set; }

    public string? CleAudType { get; set; }

    public DateTime? CleAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }
}
