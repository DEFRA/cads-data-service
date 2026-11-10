using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Entities;

public partial class CtsParallelImportWorkQueue
{
    public long WorkId { get; set; }

    public long RunId { get; set; }

    public string TableName { get; set; } = null!;

    public int DependencyDepth { get; set; }

    public long MinimumTransId { get; set; }

    public long MaximumTransId { get; set; }

    public string State { get; set; } = null!;

    public string? ClaimedBy { get; set; }

    public int? ClaimedBackendPid { get; set; }

    public DateTime? ClaimedAt { get; set; }

    public DateTime? HeartbeatAt { get; set; }

    public long RowsMigrated { get; set; }

    public long RowsDeferred { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? LastErrorMessage { get; set; }

    public virtual CtsParallelImportRun Run { get; set; } = null!;
}