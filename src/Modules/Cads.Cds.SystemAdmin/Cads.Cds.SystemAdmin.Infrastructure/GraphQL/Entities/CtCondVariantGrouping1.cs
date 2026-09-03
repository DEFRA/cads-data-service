using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtCondVariantGrouping1
{
    public decimal CvgId { get; set; }

    public decimal? CvgCovId { get; set; }

    public string? CvgGroupingCode { get; set; }

    public DateOnly? CvgCurrentModifiedDate { get; set; }

    public string? CvgCurrentStatus { get; set; }

    public string? CvgCurrentUser { get; set; }

    public decimal? CvgCurrentPid { get; set; }

    public decimal? CvgVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? CvgAudId { get; set; }

    public string? CvgAudType { get; set; }

    public DateTime? CvgAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}