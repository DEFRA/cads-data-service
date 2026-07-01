using Cads.Cds.Api.Core.Domain.Entities;
using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.Api.Testing.Support.Contexts;

public class TestApiReadDbContext(DbContextOptions<TestApiReadDbContext> options)
    : ApiReadDbContext(options)
{
    // Functions
    public DbSet<LocationSummary> LocationSummaries => Set<LocationSummary>();

    public override IQueryable<LocationSummary> GetLocationsSummary(string? cph, DateOnly? lastModifiedDate)
    {
        return LocationSummaries.Where(x =>
            (string.IsNullOrEmpty(cph) || x.LidFullIdentifier == cph) &&
            (!lastModifiedDate.HasValue || x.LocCurrentModifiedDate!.Value.DayNumber == lastModifiedDate.Value.DayNumber));
    }

    /// <summary>
    /// Give fake keys so EF Core can track them (after base.OnModelCreating)
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Functions
        modelBuilder.Entity<LocationSummary>().HasKey(x => new { x.LidFullIdentifier, x.LocCurrentModifiedDate });
    }
}