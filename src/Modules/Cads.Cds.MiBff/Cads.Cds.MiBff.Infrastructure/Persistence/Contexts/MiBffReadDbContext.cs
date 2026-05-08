using System.Diagnostics.CodeAnalysis;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;

[ExcludeFromCodeCoverage]
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

    // Functions
    public virtual IQueryable<MiEffectiveReportPermission> GetMiEffectiveReportPermission(string externalSubject, string? reportKey)
        => FromExpression(() => GetMiEffectiveReportPermission(externalSubject, reportKey));

    public virtual IQueryable<MiEffectiveReportAllPermission> GetMiEffectiveReportAllPermission(string externalSubject, string reportKey)
        => FromExpression(() => GetMiEffectiveReportAllPermission(externalSubject, reportKey));

    public virtual IQueryable<MiBirthSummary> GetBirthsSummary(DateOnly birthDateFrom, DateOnly birthDateTo)
        => FromExpression(() => GetBirthsSummary(birthDateFrom, birthDateTo));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MiBffReadDbContext).Assembly);

        modelBuilder.HasDbFunction(
            typeof(MiBffReadDbContext).GetMethod(nameof(GetMiEffectiveReportPermission))!)
            .HasName("get_mi_effective_report_permission")
            .HasSchema("public");

        modelBuilder.HasDbFunction(
            typeof(MiBffReadDbContext).GetMethod(nameof(GetMiEffectiveReportAllPermission))!)
            .HasName("get_mi_effective_report_all_permission")
            .HasSchema("public");

        modelBuilder.HasDbFunction(
            typeof(MiBffReadDbContext).GetMethod(nameof(GetBirthsSummary))!)
            .HasName("get_births_summary")
            .HasSchema("public");

        base.OnModelCreating(modelBuilder);
    }
}