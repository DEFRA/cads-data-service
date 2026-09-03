using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtPs9999AhdbMovHistory1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal? RanId { get; set; }

    public DateOnly? OnDate { get; set; }

    public DateOnly? OffDate { get; set; }

    public decimal? LocId { get; set; }

    public string? LocFullIdentifier { get; set; }

    public decimal? RowNumber { get; set; }

    public long? LocAudId { get; set; }

    public string? LocAudType { get; set; }

    public DateTime? LocAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtPs9999AhdbMovHistory2> CtPs9999AhdbMovHistory2s { get; set; } = new List<CtPs9999AhdbMovHistory2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}