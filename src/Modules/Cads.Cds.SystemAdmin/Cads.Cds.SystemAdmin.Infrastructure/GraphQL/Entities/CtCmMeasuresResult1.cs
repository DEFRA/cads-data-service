using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtCmMeasuresResult1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal? CmrComId { get; set; }

    public string? CmrResultChar { get; set; }

    public string? CmrMeasureChar { get; set; }

    public decimal? CmrResultNum { get; set; }

    public decimal? CmrMeasureNum { get; set; }

    public string? CmrCurrentUser { get; set; }

    public DateOnly? CmrCurrentModifiedDate { get; set; }

    public string? CmrCurrentStatus { get; set; }

    public decimal? CmrCurrentPid { get; set; }

    public decimal? CmrVersion { get; set; }

    public decimal CmrId { get; set; }

    public decimal? RowNumber { get; set; }

    public long? CmrAudId { get; set; }

    public string? CmrAudType { get; set; }

    public DateTime? CmrAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtCmMeasuresResult2> CtCmMeasuresResult2s { get; set; } = new List<CtCmMeasuresResult2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}