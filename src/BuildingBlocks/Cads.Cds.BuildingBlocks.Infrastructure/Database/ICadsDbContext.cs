using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database;

public interface ICadsDbContext : IDbContext
{
    DbSet<MiPermission> Permissions { get; }

    DbSet<MiReport> Reports { get; }

    DbSet<MiReportGroup> ReportGroups { get; }

    DbSet<MiRole> Roles { get; }

    DbSet<MiRoleReportPermission> RoleReportPermissions { get; }

    DbSet<MiUser> Users { get; }

    DbSet<MiUserRole> UserRoles { get; }

    DbSet<MiUserReportPermission> UserReportPermissions { get; }

    DbSet<MiEffectiveReportPermission> EffectiveReportPermissions { get; }
}