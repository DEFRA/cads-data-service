using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtStageFile1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal StfId { get; set; }

    public string? StfFileName { get; set; }

    public string? StfFileType { get; set; }

    public decimal? StfLineNumber { get; set; }

    public string? StfRecord { get; set; }

    public DateOnly? StfTimestamp { get; set; }

    public decimal? RowNumber { get; set; }

    public long? StfAudId { get; set; }

    public string? StfAudType { get; set; }

    public DateTime? StfAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtStageFile2> CtStageFile2s { get; set; } = new List<CtStageFile2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}