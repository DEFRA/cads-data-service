using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.Cads.Entities;

public partial class CtsFileImport
{
    public long CtsFileImportId { get; set; }

    public string DestinationTableName { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public long TotalRowsToProcess { get; set; }

    public DateTime AddedAt { get; set; }

    public short ImportStatusId { get; set; }

    public short ProcessingStatusId { get; set; }

    public long RowsFound { get; set; }

    public DateTime? ImportStartAt { get; set; }

    public DateTime? ImportEndAt { get; set; }

    public DateTime? ProcessingStartAt { get; set; }

    public DateTime? ProcessingEndAt { get; set; }

    public short? FailedAttempts { get; set; }

    public string? LastErrorReason { get; set; }

    public string GroupKey { get; set; } = null!;

    public string ImportType { get; set; } = null!;

    public DateTime BatchDate { get; set; }

    public long RowsImported { get; set; }

    public string? LastFilePartImported { get; set; }

    public virtual ICollection<CtsFileImportsLog> CtsFileImportsLogs { get; set; } = new List<CtsFileImportsLog>();

    public virtual CtsFileImportStatus ImportStatus { get; set; } = null!;

    public virtual CtsFileProcessingStatus ProcessingStatus { get; set; } = null!;
}