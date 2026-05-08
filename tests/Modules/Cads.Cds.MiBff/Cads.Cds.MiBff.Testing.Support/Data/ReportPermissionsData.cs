using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Testing.Support.Data;

public record ReportPermissionsData(
    List<MiUser> MiUsers,
    List<MiRole> MiRoles,
    List<MiPermission> MiPermissions,
    List<MiReport> MiReports,
    List<MiUserRole> MiUserRoles,
    List<MiRoleReportPermission> MiRoleReportPermissions,
    List<MiEffectiveReportPermission> MiEffectiveReportPermissions,
    List<MiEffectiveReportAllPermission> MiEffectiveReportAllPermissions
);