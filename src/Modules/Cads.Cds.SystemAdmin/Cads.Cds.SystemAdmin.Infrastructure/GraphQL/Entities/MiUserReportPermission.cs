using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class MiUserReportPermission
{
    public Guid UserId { get; set; }

    public Guid ReportId { get; set; }

    public Guid PermissionId { get; set; }

    public bool Granted { get; set; }

    public string? Reason { get; set; }

    public DateTime GrantedAt { get; set; }

    public virtual MiPermission Permission { get; set; } = null!;

    public virtual MiReport Report { get; set; } = null!;

    public virtual MiUser User { get; set; } = null!;
}
