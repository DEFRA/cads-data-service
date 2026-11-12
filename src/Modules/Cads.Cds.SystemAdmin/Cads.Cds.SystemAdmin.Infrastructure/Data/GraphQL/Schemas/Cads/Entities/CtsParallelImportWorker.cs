using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cads.Entities;

public partial class CtsParallelImportWorker
{
    public long RunId { get; set; }

    public string WorkerName { get; set; } = null!;

    public int? BackendPid { get; set; }

    public string Status { get; set; } = null!;

    public long? CurrentWorkId { get; set; }

    public string? CurrentTableName { get; set; }

    public long BatchesCommitted { get; set; }

    public long RowsMigrated { get; set; }

    public long RowsDeferred { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime HeartbeatAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? LastErrorMessage { get; set; }

    public virtual CtsParallelImportRun Run { get; set; } = null!;
}