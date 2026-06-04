using Cads.Cds.Api.Core.Domain.Entities;
using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.Api.Testing.Support.Contexts;

public class TestApiReadDbContext(DbContextOptions<ApiReadDbContext> options)
    : ApiReadDbContext(options)
{
    // Functions
    public DbSet<LocationSummary> LocationSummaries => Set<LocationSummary>();

    public override IQueryable<LocationSummary> GetLocationsSummary(string? cph, DateOnly? lastModifiedDate)
        => LocationSummaries;

    /// <summary>
    /// Give fake keys so EF Core can track them
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Functions
        modelBuilder.Entity<LocationSummary>().HasKey(x => new { x.LidIdentifier, x.LidFullIdentifier, x.LidCurrentModifiedDate });
    }
}
