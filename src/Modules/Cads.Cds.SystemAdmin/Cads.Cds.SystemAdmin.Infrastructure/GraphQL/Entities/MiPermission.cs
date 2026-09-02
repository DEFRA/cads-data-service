using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class MiPermission
{
    public Guid PermissionId { get; set; }

    public string PermissionKey { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<MiRoleReportPermission> MiRoleReportPermissions { get; set; } = new List<MiRoleReportPermission>();

    public virtual ICollection<MiUserReportPermission> MiUserReportPermissions { get; set; } = new List<MiUserReportPermission>();
}
