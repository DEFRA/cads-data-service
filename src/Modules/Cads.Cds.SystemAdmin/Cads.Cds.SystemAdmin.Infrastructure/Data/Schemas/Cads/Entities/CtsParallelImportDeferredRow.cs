using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Entities;

public partial class CtsParallelImportDeferredRow
{
    public long RunId { get; set; }

    public string TableName { get; set; } = null!;

    public long TransactionRowId { get; set; }

    public int DependencyDepth { get; set; }

    public string Status { get; set; } = null!;

    public string? BulkErrorMessage { get; set; }

    public int RetryCount { get; set; }

    public string? ErrorSqlstate { get; set; }

    public string? ErrorMessage { get; set; }

    public string? ErrorDetail { get; set; }

    public string? ErrorHint { get; set; }

    public string? ErrorContext { get; set; }

    public DateTime DeferredAt { get; set; }

    public DateTime? LastAttemptedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public virtual CtsParallelImportRun Run { get; set; } = null!;
}