using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsAudit.Entities;

public partial class CtLocrestrictionstoanimal
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal? LraComId { get; set; }

    public DateOnly? LraLastProbityDate { get; set; }

    public DateOnly? LraComEffectiveFrom { get; set; }

    public DateOnly? LraComEffectiveTo { get; set; }

    public decimal? LraLocId { get; set; }

    public decimal? LraRanId { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
