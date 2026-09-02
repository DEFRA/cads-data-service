using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtProbityCheck1
{
    public decimal PchId { get; set; }

    public string? PchLongDescription { get; set; }

    public string? PchShortDescription { get; set; }

    public DateOnly? PchCheckedToDate { get; set; }

    public decimal? PchCheckPeriod { get; set; }

    public DateOnly? PchNextCheckDate { get; set; }

    public string? PchCurrentUser { get; set; }

    public string? PchCurrentStatus { get; set; }

    public DateOnly? PchCurrentModifiedDate { get; set; }

    public decimal? PchCurrentPid { get; set; }

    public decimal? PchVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public long? PchAudId { get; set; }

    public string? PchAudType { get; set; }

    public DateTime? PchAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
