using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtSuspCmMeasureResult1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal SmrId { get; set; }

    public decimal? SmrScmId { get; set; }

    public string? SmrMeasureChar { get; set; }

    public decimal? SmrResultNum { get; set; }

    public decimal? SmrMeasureNum { get; set; }

    public string? SmrResultChar { get; set; }

    public string? SmrCurrentStatus { get; set; }

    public DateOnly? SmrCurrentModifiedDate { get; set; }

    public string? SmrCurrentUser { get; set; }

    public decimal? SmrCurrentPid { get; set; }

    public decimal? SmrVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? SmrAudId { get; set; }

    public string? SmrAudType { get; set; }

    public DateTime? SmrAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtSuspCmMeasureResult2> CtSuspCmMeasureResult2s { get; set; } = new List<CtSuspCmMeasureResult2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}