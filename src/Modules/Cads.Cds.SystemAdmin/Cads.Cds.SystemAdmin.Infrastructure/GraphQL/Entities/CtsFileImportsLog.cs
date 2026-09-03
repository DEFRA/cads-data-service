using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtsFileImportsLog
{
    public long CtsFileImportLogId { get; set; }

    public long CtsFileImportId { get; set; }

    public string LogLevel { get; set; } = null!;

    public string LogMessage { get; set; } = null!;

    public string? ErrorMessage { get; set; }

    public long? ExpectedRecords { get; set; }

    public long? ProcessedRecords { get; set; }

    public long InsertedRecords { get; set; }

    public long UpdatedRecords { get; set; }

    public long DeletedRecords { get; set; }

    public DateTime? ProcessingStartedAt { get; set; }

    public DateTime? ProcessingEndedAt { get; set; }

    public DateTime? InsertStartedAt { get; set; }

    public DateTime? InsertEndedAt { get; set; }

    public DateTime? UpdateStartedAt { get; set; }

    public DateTime? UpdateEndedAt { get; set; }

    public DateTime? DeleteStartedAt { get; set; }

    public DateTime? DeleteEndedAt { get; set; }

    public DateTime LoggedAt { get; set; }

    public virtual CtsFileImport CtsFileImport { get; set; } = null!;
}