using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class MiRole
{
    public Guid RoleId { get; set; }

    public string RoleKey { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<MiRoleReportPermission> MiRoleReportPermissions { get; set; } = new List<MiRoleReportPermission>();

    public virtual ICollection<MiUserRole> MiUserRoles { get; set; } = new List<MiUserRole>();
}