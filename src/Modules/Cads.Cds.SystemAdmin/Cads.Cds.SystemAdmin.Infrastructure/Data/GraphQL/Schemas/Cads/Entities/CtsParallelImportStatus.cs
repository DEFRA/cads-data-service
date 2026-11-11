using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cads.Entities;

public partial class CtsParallelImportStatus
{
    public long RunId { get; set; }

    public string TableName { get; set; } = null!;

    public int DependencyDepth { get; set; }

    public long OriginalTotal { get; set; }

    public long NumberMigrated { get; set; }

    public long NumberDeferred { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? StartedAt { get; set; }

    public DateTime? BulkCompletedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? LastErrorMessage { get; set; }

    public virtual CtsParallelImportRun Run { get; set; } = null!;
}