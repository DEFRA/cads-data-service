using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class MiUser
{
    public Guid UserId { get; set; }

    public string ExternalSubject { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string? Email { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? ExternalSubjectNormalized { get; set; }

    public virtual ICollection<MiUserReportPermission> MiUserReportPermissions { get; set; } = new List<MiUserReportPermission>();

    public virtual ICollection<MiUserRole> MiUserRoles { get; set; } = new List<MiUserRole>();
}