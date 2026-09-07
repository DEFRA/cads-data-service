using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtExtCetdEartag
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public string? CetKey { get; set; }

    public string? CetHerd { get; set; }

    public decimal? CetRsc { get; set; }

    public DateOnly? CetDate { get; set; }

    public string? CetBsps { get; set; }

    public string? CetCid { get; set; }

    public string? CetScps { get; set; }

    public decimal? CetVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? CetAudId { get; set; }

    public string? CetAudType { get; set; }

    public DateTime? CetAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }
}