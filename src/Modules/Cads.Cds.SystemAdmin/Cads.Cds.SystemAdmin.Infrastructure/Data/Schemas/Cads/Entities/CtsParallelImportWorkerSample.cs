using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Entities;

/// <summary>
/// Transactional worker progress history. observed_at is sampled before commit, not the WAL flush completion time. Counter differences support comparisons across time windows.
/// </summary>
public partial class CtsParallelImportWorkerSample
{
    public long SampleId { get; set; }

    public long RunId { get; set; }

    public string WorkerName { get; set; } = null!;

    public int? BackendPid { get; set; }

    public DateTime WorkerStartedAt { get; set; }

    public DateTime ObservedAt { get; set; }

    public string Status { get; set; } = null!;

    public string? TableName { get; set; }

    public long? WorkId { get; set; }

    public long BatchesCommitted { get; set; }

    public long RowsMigrated { get; set; }

    public long RowsDeferred { get; set; }

    public DateTime? WorkerCompletedAt { get; set; }

    public bool BaselineOnly { get; set; }
}