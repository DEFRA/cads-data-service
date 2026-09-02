using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtLocationsFaker2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public string? LocTelNumber { get; set; }

    public string? LocMobileNumber { get; set; }

    public string? LocFaxNumber { get; set; }

    public string? LocEmailAddress { get; set; }

    public string? LocSourceReference { get; set; }

    public string? LocComments { get; set; }

    public long? TransId { get; set; }

    public virtual CtLocationsFaker1? AuditTrans { get; set; }
}
