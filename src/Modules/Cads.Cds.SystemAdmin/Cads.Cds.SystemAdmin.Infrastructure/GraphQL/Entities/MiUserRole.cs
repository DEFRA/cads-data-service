using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class MiUserRole
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public DateTime GrantedAt { get; set; }

    public virtual MiRole Role { get; set; } = null!;

    public virtual MiUser User { get; set; } = null!;
}
