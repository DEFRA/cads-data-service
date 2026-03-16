using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database;

public abstract class BaseDbContext(DbContextOptions options) : DbContext(options)
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
    public DbSet<MiEffectiveReportPermission> EffectiveReportPermissions => Set<MiEffectiveReportPermission>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MiUserConfiguration());
        modelBuilder.ApplyConfiguration(new MiRoleConfiguration());
        modelBuilder.ApplyConfiguration(new MiUserRoleConfiguration());

        modelBuilder.ApplyConfiguration(new MiReportConfiguration());
        modelBuilder.ApplyConfiguration(new MiReportGroupConfiguration());
        modelBuilder.ApplyConfiguration(new MiReportGroupMapConfiguration());

        modelBuilder.ApplyConfiguration(new MiPermissionConfiguration());
        modelBuilder.ApplyConfiguration(new MiRoleReportPermissionConfiguration());
        modelBuilder.ApplyConfiguration(new MiUserReportPermissionConfiguration());

        modelBuilder.ApplyConfiguration(new MiEffectiveReportPermissionConfiguration());

        base.OnModelCreating(modelBuilder);

        ConfigureModel(modelBuilder); // Allow child contexts to add their own config
    }

    // Abstract method for child-specific configuration
    protected abstract void ConfigureModel(ModelBuilder modelBuilder);
}