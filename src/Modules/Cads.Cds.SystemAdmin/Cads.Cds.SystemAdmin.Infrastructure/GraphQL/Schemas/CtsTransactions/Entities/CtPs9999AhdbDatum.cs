using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsTransactions.Entities;

public partial class CtPs9999AhdbDatum
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal RanId { get; set; }

    public string? CurrentCph { get; set; }

    public string? AnimalEartag { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? BreedCode { get; set; }

    public string? SexOfAnimal { get; set; }

    public decimal? RowNumber { get; set; }

    public long? RanAudId { get; set; }

    public string? RanAudType { get; set; }

    public DateTime? RanAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }
}
