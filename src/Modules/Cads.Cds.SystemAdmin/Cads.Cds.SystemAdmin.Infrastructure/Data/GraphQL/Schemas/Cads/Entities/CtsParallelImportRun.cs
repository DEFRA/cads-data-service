using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cads.Entities;

public partial class CtsParallelImportRun
{
    public long RunId { get; set; }

    public string Status { get; set; } = null!;

    public bool DeleteSource { get; set; }

    public long RangeSize { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? BulkCompletedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? LastErrorMessage { get; set; }

    public bool TruncateCts { get; set; }

    public virtual ICollection<CtsParallelImportConstraintRestoreLog> CtsParallelImportConstraintRestoreLogs { get; set; } = new List<CtsParallelImportConstraintRestoreLog>();

    public virtual ICollection<CtsParallelImportDeferredRow> CtsParallelImportDeferredRows { get; set; } = new List<CtsParallelImportDeferredRow>();

    public virtual ICollection<CtsParallelImportDependency> CtsParallelImportDependencies { get; set; } = new List<CtsParallelImportDependency>();

    public virtual ICollection<CtsParallelImportStatus> CtsParallelImportStatuses { get; set; } = new List<CtsParallelImportStatus>();

    public virtual ICollection<CtsParallelImportWorkQueue> CtsParallelImportWorkQueues { get; set; } = new List<CtsParallelImportWorkQueue>();

    public virtual ICollection<CtsParallelImportWorker> CtsParallelImportWorkers { get; set; } = new List<CtsParallelImportWorker>();
}