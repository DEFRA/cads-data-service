using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Tests.Component.TestFixtures;

internal class TestMiBffReadDbContext(DbContextOptions<MiBffReadDbContext> options)
    : MiBffReadDbContext(options)
{
    public DbSet<MiEffectiveReportPermissionView> EffectiveReportPermissions
        => Set<MiEffectiveReportPermissionView>();

    public DbSet<MiEffectiveReportAllPermissionView> EffectiveReportAllPermissions
        => Set<MiEffectiveReportAllPermissionView>();

    public override IQueryable<MiEffectiveReportPermissionView> GetMiEffectiveReportPermission(
        string externalSubject, string? reportKey)
    {
        return EffectiveReportPermissions.AsQueryable()
            .Where(x => x.ExternalSubject == externalSubject && (reportKey == null || x.ReportKey == reportKey));
    }

    public override IQueryable<MiEffectiveReportAllPermissionView> GetMiEffectiveReportAllPermission(
        string externalSubject, string reportKey)
    {
        return EffectiveReportAllPermissions.AsQueryable()
            .Where(x => x.ExternalSubject == externalSubject && x.ReportKey == reportKey);
    }

}