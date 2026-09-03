using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLabelSummary1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal LasId { get; set; }

    public decimal? LasLocIdIdentifying { get; set; }

    public decimal? LasLocIdLabels { get; set; }

    public decimal? LasLabelVersionNumber { get; set; }

    public DateOnly? LasLastSubmittedDate { get; set; }

    public string? LasDefaultLabelType { get; set; }

    public decimal? LasDefaultSheetQuantity { get; set; }

    public string? LasCurrentUser { get; set; }

    public string? LasCurrentStatus { get; set; }

    public DateOnly? LasCurrentModifiedDate { get; set; }

    public decimal? LasCurrentPid { get; set; }

    public decimal? LasVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? LasAudId { get; set; }

    public string? LasAudType { get; set; }

    public DateTime? LasAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtLabelSummary2> CtLabelSummary2s { get; set; } = new List<CtLabelSummary2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}