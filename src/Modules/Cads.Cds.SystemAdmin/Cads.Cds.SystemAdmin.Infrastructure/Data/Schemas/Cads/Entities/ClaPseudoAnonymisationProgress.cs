using System;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.Schemas.Cads.Entities;

public partial class ClaPseudoAnonymisationProgress
{
    public long ProgressId { get; set; }

    public long RunId { get; set; }

    public string SchemaName { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public long? RowsAnonymised { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? FailedAt { get; set; }

    public string? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public int? ElapsedHours { get; set; }

    public int? ElapsedMinutes { get; set; }
}