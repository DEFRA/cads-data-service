using System;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Entities;

public partial class MiReport
{
    public Guid ReportId { get; set; }

    public string ReportKey { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<MiRoleReportPermission> MiRoleReportPermissions { get; set; } = new List<MiRoleReportPermission>();

    public virtual ICollection<MiUserReportPermission> MiUserReportPermissions { get; set; } = new List<MiUserReportPermission>();

    public virtual ICollection<MiReportGroup> Groups { get; set; } = new List<MiReportGroup>();
}