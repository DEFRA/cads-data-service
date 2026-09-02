using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtMovtCorrSummError1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal MseId { get; set; }

    public decimal? MseMcsId { get; set; }

    public string? MseCurrentUser { get; set; }

    public string? MseCurrentStatus { get; set; }

    public DateOnly? MseCurrentModifiedDate { get; set; }

    public decimal? MseCurrentPid { get; set; }

    public string? MseAttributeName { get; set; }

    public string? MseErrorCode { get; set; }

    public decimal? MseVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? MseAudId { get; set; }

    public string? MseAudType { get; set; }

    public DateTime? MseAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtMovtCorrSummError2> CtMovtCorrSummError2s { get; set; } = new List<CtMovtCorrSummError2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}
