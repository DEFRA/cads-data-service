using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cads.Entities;

public partial class CtsParallelImportWorkerMetric
{
    public long? RunId { get; set; }

    public string? WorkerName { get; set; }

    public DateTime? StartedAt { get; set; }

    public int? BackendPid { get; set; }

    public string? Status { get; set; }

    public DateTime? MeasuredFrom { get; set; }

    public DateTime? LastProgressAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public bool? PartialHistory { get; set; }

    public long? MigratedRows { get; set; }

    public long? DeferredRows { get; set; }

    public long? SuccessfulBatches { get; set; }

    public string? ExecutionState { get; set; }

    public decimal? ElapsedSeconds { get; set; }

    public decimal? RowsPerSecond { get; set; }

    public decimal? RowsPerMinute { get; set; }

    public decimal? ElapsedSecondsPerSuccessfulBatch { get; set; }
}