using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class MiRoleReportPermission
{
    public Guid RoleId { get; set; }

    public Guid ReportId { get; set; }

    public Guid PermissionId { get; set; }

    public bool Granted { get; set; }

    public virtual MiPermission Permission { get; set; } = null!;

    public virtual MiReport Report { get; set; } = null!;

    public virtual MiRole Role { get; set; } = null!;
}
