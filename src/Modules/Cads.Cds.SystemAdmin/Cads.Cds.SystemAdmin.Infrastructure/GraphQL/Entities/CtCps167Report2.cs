using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtCps167Report2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal KnsId { get; set; }

    public DateOnly? KnsRunDateTime { get; set; }

    public string? KnsFilename { get; set; }

    public string? KnsActionType { get; set; }

    public string? KnsSourceDirectory { get; set; }

    public string? KnsDestinationDirectory { get; set; }

    public string? KnsMessage { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtCps167Report1? AuditTrans { get; set; }
}
