using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtEartagStaging2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public decimal EstId { get; set; }

    public string? EstEartag { get; set; }

    public string? EstUsageCode { get; set; }

    public string? EstIdentifierAvailability { get; set; }

    public string? EstOrderLocationRepd { get; set; }

    public decimal? EstLocIdOrder { get; set; }

    public string? EstEartagReasonCode { get; set; }

    public decimal? EstErfId { get; set; }

    public DateOnly? EstCurrentModifiedDate { get; set; }

    public long? TransId { get; set; }

    public virtual CtEartagStaging1? AuditTrans { get; set; }
}