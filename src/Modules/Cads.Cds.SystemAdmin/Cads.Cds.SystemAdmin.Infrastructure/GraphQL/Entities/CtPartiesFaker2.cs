using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class CtPartiesFaker2
{
    public long AuditId { get; set; }

    public string AuditAction { get; set; } = null!;

    public long? AuditTransId { get; set; }

    public DateTime AuditedAt { get; set; }

    public string? ParSurname { get; set; }

    public string? ParInitials { get; set; }

    public string? ParTitle { get; set; }

    public string? ParTelNumber { get; set; }

    public string? ParMobileNumber { get; set; }

    public string? ParEmailAddress { get; set; }

    public long? TransId { get; set; }

    public virtual CtPartiesFaker1? AuditTrans { get; set; }
}