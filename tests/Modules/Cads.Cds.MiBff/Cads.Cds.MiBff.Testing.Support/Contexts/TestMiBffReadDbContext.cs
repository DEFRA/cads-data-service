using Cads.Cds.BuildingBlocks.Testing.Support.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Testing.Support.Contexts;

public class TestMiBffReadDbContext(DbContextOptions<MiBffReadDbContext> options)
    : MiBffReadDbContext(options)
{
    public DbSet<MiEffectiveReportPermission> EffectiveReportPermissions
        => Set<MiEffectiveReportPermission>();

    public DbSet<MiEffectiveReportAllPermission> EffectiveReportAllPermissions
        => Set<MiEffectiveReportAllPermission>();

    public List<MiBirthSummary> BirthSummaries { get; set; } = [];

    public List<MiDeathSummary> DeathSummaries { get; set; } = [];

    public override IQueryable<MiEffectiveReportPermission> GetMiEffectiveReportPermission(
        string externalSubject, string? reportKey)
    {
        return EffectiveReportPermissions.AsQueryable()
            .Where(x => x.ExternalSubject == externalSubject && (reportKey == null || x.ReportKey == reportKey));
    }

    public override IQueryable<MiEffectiveReportAllPermission> GetMiEffectiveReportAllPermission(
        string externalSubject, string reportKey)
    {
        return EffectiveReportAllPermissions.AsQueryable()
            .Where(x => x.ExternalSubject == externalSubject && x.ReportKey == reportKey);
    }

    public override IQueryable<MiBirthSummary> GetBirthsSummary(
        DateOnly birthDateFrom, DateOnly birthDateTo)
    {
        return new TestAsyncEnumerable<MiBirthSummary>(BirthSummaries);
    }

    public override IQueryable<MiDeathSummary> GetDeathsSummary(
        DateOnly deathDateFrom, DateOnly deathDateTo)
    {
        return new TestAsyncEnumerable<MiDeathSummary>(DeathSummaries);
    }
}