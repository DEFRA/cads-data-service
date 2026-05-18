using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;

namespace Cads.Cds.MiBff.Testing.Support.Contexts;

public class TestMiBffReadDbContext(DbContextOptions<MiBffReadDbContext> options)
    : MiBffReadDbContext(options)
{
    // Mi report permissions
    public DbSet<MiEffectiveReportPermission> EffectiveReportPermissions => Set<MiEffectiveReportPermission>();
    public DbSet<MiEffectiveReportAllPermission> EffectiveReportAllPermissions => Set<MiEffectiveReportAllPermission>();

    public override IQueryable<MiEffectiveReportPermission> GetMiEffectiveReportPermission(string externalSubject, string? reportKey)
        => EffectiveReportPermissions.Where(x => x.ExternalSubject == externalSubject && (reportKey == null || x.ReportKey == reportKey));

    public override IQueryable<MiEffectiveReportAllPermission> GetMiEffectiveReportAllPermission(string externalSubject, string reportKey)
        => EffectiveReportAllPermissions.Where(x => x.ExternalSubject == externalSubject && x.ReportKey == reportKey);

    // GB births and deaths
    public DbSet<MiBirthSummary> BirthSummaries => Set<MiBirthSummary>();
    public DbSet<MiDeathSummary> DeathSummaries => Set<MiDeathSummary>();
    public DbSet<MiImportSummary> ImportSummaries => Set<MiImportSummary>();

    public override IQueryable<MiBirthSummary> GetBirthsSummary(DateOnly from, DateOnly to)
        => BirthSummaries.Where(x => x.BirthYear >= from.Year && x.BirthYear <= to.Year);

    public override IQueryable<MiDeathSummary> GetDeathsSummary(DateOnly from, DateOnly to)
        => DeathSummaries.Where(x => x.DeathYear >= from.Year && x.DeathYear <= to.Year);

    public override IQueryable<MiImportSummary> GetImportsSummary(DateOnly from, DateOnly to)
        => ImportSummaries.Where(x => x.MonthYear >= from.ToDateTime(TimeOnly.MinValue) && x.MonthYear < to.ToDateTime(TimeOnly.MinValue));

    /// <summary>
    /// Give fake keys so EF Core can track them
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mi report permissions
        modelBuilder.Entity<MiEffectiveReportPermission>().HasKey(x => new { x.ExternalSubject, x.ReportKey });
        modelBuilder.Entity<MiEffectiveReportAllPermission>().HasKey(x => new { x.ExternalSubject, x.ReportKey });

        // GB report entities
        modelBuilder.Entity<MiBirthSummary>().HasKey(x => new { x.BirthYear, x.BirthMonth, x.Country });
        modelBuilder.Entity<MiDeathSummary>().HasKey(x => new { x.DeathYear, x.Country });
        modelBuilder.Entity<MiImportSummary>().HasKey(x => new { x.MonthYear, x.Country, x.AgeAtImport, x.BreedType, x.Sex });
    }
}