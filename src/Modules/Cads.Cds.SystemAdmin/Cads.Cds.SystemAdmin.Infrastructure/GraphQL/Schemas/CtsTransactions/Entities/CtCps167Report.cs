using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtCps167Report
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal KnsId { get; set; }

    public DateOnly? KnsRunDateTime { get; set; }

    public string? KnsFilename { get; set; }

    public string? KnsActionType { get; set; }

    public string? KnsSourceDirectory { get; set; }

    public string? KnsDestinationDirectory { get; set; }

    public string? KnsMessage { get; set; }

    public decimal? RowNumber { get; set; }

    public long? KnsAudId { get; set; }

    public string? KnsAudType { get; set; }

    public DateTime? KnsAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }
}