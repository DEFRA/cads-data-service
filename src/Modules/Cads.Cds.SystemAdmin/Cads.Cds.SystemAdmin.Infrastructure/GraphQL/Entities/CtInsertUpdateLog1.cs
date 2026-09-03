using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtInsertUpdateLog1
{
    public long TransId { get; set; }

    public string TransType { get; set; } = null!;

    public decimal IulId { get; set; }

    public string? IulSystem { get; set; }

    public string? IulTableName { get; set; }

    public string? IulRecordKey { get; set; }

    public string? IulName { get; set; }

    public DateOnly? IulDateProcessed { get; set; }

    public DateOnly? IulDateProcessedMis { get; set; }

    public string? IulInsertDeleteFlag { get; set; }

    public string? IulCurrentUser { get; set; }

    public string? IulCurrentStatus { get; set; }

    public DateOnly? IulCurrentModifiedDate { get; set; }

    public decimal? IulCurrentPid { get; set; }

    public decimal? IulVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? IulAudId { get; set; }

    public string? IulAudType { get; set; }

    public DateTime? IulAudDatetime { get; set; }

    public string? RecordType { get; set; }

    public decimal? RecordCount { get; set; }

    public DateTime? ImportedDate { get; set; }

    public long? CtsFileImportId { get; set; }

    public virtual ICollection<CtInsertUpdateLog2> CtInsertUpdateLog2s { get; set; } = new List<CtInsertUpdateLog2>();

    public virtual CtsFileImport? CtsFileImport { get; set; }
}