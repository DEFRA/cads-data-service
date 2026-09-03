using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtElectronicIdentifier2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal EidId { get; set; }

    public decimal? EidElectronicIdentifier { get; set; }

    public decimal? EidIsaId { get; set; }

    public string? EidUniqueNumber { get; set; }

    public string? EidCurrentStatus { get; set; }

    public string? EidCurrentUser { get; set; }

    public decimal? EidCurrentPid { get; set; }

    public DateOnly? EidCurrentModifiedDate { get; set; }

    public decimal? EidVersion { get; set; }

    public decimal? RowNumber { get; set; }

    public long? TransId { get; set; }

    public virtual CtElectronicIdentifier1? AuditTrans { get; set; }
}