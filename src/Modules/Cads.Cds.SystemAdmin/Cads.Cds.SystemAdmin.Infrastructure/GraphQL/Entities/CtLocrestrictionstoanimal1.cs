using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocrestrictionstoanimal1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal? LraComId { get; set; }

    public DateOnly? LraLastProbityDate { get; set; }

    public DateOnly? LraComEffectiveFrom { get; set; }

    public DateOnly? LraComEffectiveTo { get; set; }

    public decimal? LraLocId { get; set; }

    public decimal? LraRanId { get; set; }

    public decimal? RowNumber { get; set; }

    public long? LraAudId { get; set; }

    public string? LraAudType { get; set; }

    public DateTime? LraAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtLocrestrictionstoanimal2> CtLocrestrictionstoanimal2s { get; set; } = new List<CtLocrestrictionstoanimal2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}