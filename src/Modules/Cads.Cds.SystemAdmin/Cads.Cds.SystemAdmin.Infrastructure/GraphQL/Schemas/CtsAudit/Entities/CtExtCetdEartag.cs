using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsAudit.Entities;

public partial class CtExtCetdEartag
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public string? CetKey { get; set; }

    public string? CetHerd { get; set; }

    public decimal? CetRsc { get; set; }

    public DateOnly? CetDate { get; set; }

    public string? CetBsps { get; set; }

    public string? CetCid { get; set; }

    public string? CetScps { get; set; }

    public decimal? CetVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
