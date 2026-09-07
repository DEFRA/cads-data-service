using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Schemas.CtsAudit.Entities;

public partial class CtPs9999AhdbDatum
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal RanId { get; set; }

    public string? CurrentCph { get; set; }

    public string? AnimalEartag { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? BreedCode { get; set; }

    public string? SexOfAnimal { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }
}
