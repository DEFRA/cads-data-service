using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;

[ExcludeFromCodeCoverage]
public class MiBffWriteDbContext(DbContextOptions<MiBffWriteDbContext> options) : DbContext(options)
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

    // Functions
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MiBffWriteDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}