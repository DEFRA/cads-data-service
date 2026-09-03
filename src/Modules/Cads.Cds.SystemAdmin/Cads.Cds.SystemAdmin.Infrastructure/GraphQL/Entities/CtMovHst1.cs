using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtMovHst1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public DateOnly? HstOndate { get; set; }

    public decimal? HstOntype { get; set; }

    public string? HstOnsource { get; set; }

    public decimal? HstOffkey { get; set; }

    public DateOnly? HstOffdate { get; set; }

    public decimal? HstOfftype { get; set; }

    public string? HstOffsource { get; set; }

    public string? HstPairind { get; set; }

    public string? HstSplitflg { get; set; }

    public decimal? HstKey { get; set; }

    public decimal? HstLkey { get; set; }

    public decimal? HstOnkey { get; set; }

    public decimal? RowNumber { get; set; }

    public long? HstAudId { get; set; }

    public string? HstAudType { get; set; }

    public DateTime? HstAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtMovHst2> CtMovHst2s { get; set; } = new List<CtMovHst2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}