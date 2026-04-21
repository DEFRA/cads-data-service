using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;

public class MiBffReadDbContext(DbContextOptions<MiBffReadDbContext> options) : DbContext(options)
{
    // Tables
    public DbSet<MiPermission> Permissions => Set<MiPermission>();

    public DbSet<MiReport> Reports => Set<MiReport>();

    public DbSet<MiReportGroup> ReportGroups => Set<MiReportGroup>();

    public DbSet<MiRole> Roles => Set<MiRole>();

    public DbSet<MiRoleReportPermission> RoleReportPermissions => Set<MiRoleReportPermission>();

    public DbSet<MiUser> Users => Set<MiUser>();

    public DbSet<MiUserRole> UserRoles => Set<MiUserRole>();

    public DbSet<MiUserReportPermission> UserReportPermissions => Set<MiUserReportPermission>();

    // Views
    public DbSet<MiEffectiveReportPermissionView> EffectiveReportPermissions => Set<MiEffectiveReportPermissionView>();
    public DbSet<MiEffectiveReportAllPermissionView> EffectiveReportAllPermissions => Set<MiEffectiveReportAllPermissionView>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MiBffReadDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}