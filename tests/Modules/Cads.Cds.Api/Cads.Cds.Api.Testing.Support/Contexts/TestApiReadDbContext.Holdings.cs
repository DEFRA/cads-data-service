using Cads.Cds.Api.Core.Domain.Entities.Holdings;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.Api.Testing.Support.Contexts;

public partial class TestApiReadDbContext
{
    public DbSet<LocationSummary> LocationSummaries => Set<LocationSummary>();

    public override IQueryable<LocationSummary> GetLocationsSummary(string? cph, DateOnly? lastModifiedDate)
        => LocationSummaries.Where(x =>
            (string.IsNullOrEmpty(cph) || x.LidFullIdentifier == cph) &&
            (!lastModifiedDate.HasValue || x.LocCurrentModifiedDate!.Value.DayNumber == lastModifiedDate.Value.DayNumber));

    private static void ConfigureHoldingFunctions(ModelBuilder modelBuilder)
        => modelBuilder.Entity<LocationSummary>()
            .HasKey(x => new { x.LidFullIdentifier, x.LocCurrentModifiedDate });
}