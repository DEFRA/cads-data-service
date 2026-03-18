using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Tests.Unit.Repositories;

internal static class TestDataSeeder
{
    // Fixed GUIDs so relationships align in tests
    public static readonly Guid User1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
    public static readonly Guid User2 = Guid.Parse("11111111-1111-1111-1111-111111111112");

    public static readonly Guid RoleAdmin = Guid.Parse("22222222-2222-2222-2222-222222222222");
    public static readonly Guid RoleViewer = Guid.Parse("22222222-2222-2222-2222-222222222223");

    public static readonly Guid ReportA = Guid.Parse("33333333-3333-3333-3333-333333333333");
    public static readonly Guid ReportB = Guid.Parse("33333333-3333-3333-3333-333333333334");

    public static readonly Guid PermissionView = Guid.Parse("44444444-4444-4444-4444-444444444444");
    public static readonly Guid PermissionEdit = Guid.Parse("44444444-4444-4444-4444-444444444445");

    public static readonly Guid GroupOps = Guid.Parse("55555555-5555-5555-5555-555555555555");

    // Public entry: seed a live DbContext (used by unit tests with in-memory/SQLite)
    public static void Seed(DbContext context)
    {
        // Add in an order that respects FK constraints
        context.AddRange(GetRoles());
        context.AddRange(GetPermissions());
        context.AddRange(GetReports());
        context.AddRange(GetReportGroups());
        context.AddRange(GetReportGroupMaps());
        context.AddRange(GetUsers());
        context.AddRange(GetRoleReportPermissions());
        context.AddRange(GetUserRoles());
        context.AddRange(GetUserReportPermissions());
        context.AddRange(GetEffectiveReportPermissions());
        context.SaveChanges();
    }

    private static MiUser[] GetUsers() =>
    [
        new MiUser
        {
            UserId = User1,
            ExternalSubject = "ext-sub-1",
            DisplayName = "Test User 1",
            Email = "user1@example.test",
            IsActive = true,
            CreatedAt = new DateTimeOffset(2023,1,1,0,0,0, TimeSpan.Zero)
        },
        new MiUser
        {
            UserId = User2,
            ExternalSubject = "ext-sub-2",
            DisplayName = "Test User 2",
            Email = "user2@example.test",
            IsActive = true,
            CreatedAt = new DateTimeOffset(2023,2,1,0,0,0, TimeSpan.Zero)
        }
    ];

    private static MiRole[] GetRoles() =>
    [
        new MiRole
        {
            RoleId = RoleAdmin,
            RoleKey = "admin",
            Description = "Administrator role",
            CreatedAt = new DateTimeOffset(2023,1,2,0,0,0, TimeSpan.Zero)
        },
        new MiRole
        {
            RoleId = RoleViewer,
            RoleKey = "viewer",
            Description = "Read-only role",
            CreatedAt = new DateTimeOffset(2023,1,3,0,0,0, TimeSpan.Zero)
        }
    ];

    private static MiReport[] GetReports() =>
    [
        new MiReport
        {
            ReportId = ReportA,
            ReportKey = "report_a",
            Title = "Report A",
            Description = "Sample report A",
            IsActive = true,
            CreatedAt = new DateTimeOffset(2023,3,1,0,0,0, TimeSpan.Zero)
        },
        new MiReport
        {
            ReportId = ReportB,
            ReportKey = "report_b",
            Title = "Report B",
            Description = "Sample report B",
            IsActive = true,
            CreatedAt = new DateTimeOffset(2023,3,2,0,0,0, TimeSpan.Zero)
        }
    ];

    private static MiReportGroup[] GetReportGroups() =>
    [
        new MiReportGroup
        {
            GroupId = GroupOps,
            GroupKey = "operations",
            Title = "Operations Reports"
        }
    ];

    private static MiReportGroupMap[] GetReportGroupMaps() =>
    [
        new MiReportGroupMap
        {
            GroupId = GroupOps,
            ReportId = ReportA
        },
        new MiReportGroupMap
        {
            GroupId = GroupOps,
            ReportId = ReportB
        }
    ];

    private static MiPermission[] GetPermissions() =>
    [
        new MiPermission
        {
            PermissionId = PermissionView,
            PermissionKey = "view_reports",
            Description = "Allows viewing reports"
        },
        new MiPermission
        {
            PermissionId = PermissionEdit,
            PermissionKey = "edit_reports",
            Description = "Allows editing reports"
        }
    ];

    private static MiRoleReportPermission[] GetRoleReportPermissions() =>
    [
        new MiRoleReportPermission
        {
            RoleId = RoleAdmin,
            ReportId = ReportA,
            PermissionId = PermissionView,
            Granted = true
        },
        new MiRoleReportPermission
        {
            RoleId = RoleAdmin,
            ReportId = ReportA,
            PermissionId = PermissionEdit,
            Granted = true
        },
        new MiRoleReportPermission
        {
            RoleId = RoleViewer,
            ReportId = ReportA,
            PermissionId = PermissionView,
            Granted = true
        }
    ];

    private static MiUserRole[] GetUserRoles() =>
    [
        new MiUserRole
        {
            UserId = User1,
            RoleId = RoleAdmin,
            GrantedAt = new DateTimeOffset(2023,4,1,0,0,0, TimeSpan.Zero)
        },
        new MiUserRole
        {
            UserId = User2,
            RoleId = RoleViewer,
            GrantedAt = new DateTimeOffset(2023,4,2,0,0,0, TimeSpan.Zero)
        }
    ];

    private static MiUserReportPermission[] GetUserReportPermissions() =>
    [
        new MiUserReportPermission
        {
            UserId = User1,
            ReportId = ReportA,
            PermissionId = PermissionView,
            Granted = true,
            Reason = "Direct grant for testing",
            GrantedAt = new DateTimeOffset(2023,5,1,0,0,0, TimeSpan.Zero)
        },
        new MiUserReportPermission
        {
            UserId = User2,
            ReportId = ReportB,
            PermissionId = PermissionView,
            Granted = true,
            Reason = "Direct grant for testing",
            GrantedAt = new DateTimeOffset(2023,5,1,0,0,0, TimeSpan.Zero)
        }
    ];

    private static MiEffectiveReportPermission[] GetEffectiveReportPermissions() =>
    [
        new MiEffectiveReportPermission
        {
            UserId = User1,
            ReportId = ReportA,
            PermissionId = PermissionView,
            Granted = true
        },
        new MiEffectiveReportPermission
        {
            UserId = User2,
            ReportId = ReportB,
            PermissionId = PermissionView,
            Granted = true
        }
    ];
}