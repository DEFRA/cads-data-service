using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.GraphQL.Schemas.Cads.Entities;

public partial class CtsParallelImportConstraintRestoreLog
{
    public long RunId { get; set; }

    public string TableName { get; set; } = null!;

    public string ConstraintName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? ErrorMessage { get; set; }

    public DateTime AttemptedAt { get; set; }

    public virtual CtsParallelImportRun Run { get; set; } = null!;
}