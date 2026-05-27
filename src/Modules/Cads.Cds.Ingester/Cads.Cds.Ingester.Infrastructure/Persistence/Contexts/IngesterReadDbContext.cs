using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Cads.Cds.Ingester.Core.Domain.Entities;

namespace Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;

[ExcludeFromCodeCoverage]
public class IngesterReadDbContext(DbContextOptions<IngesterReadDbContext> options) : DbContext(options)
{
    // Shared canonical entities

    // Module-specific entities
    public DbSet<DataSeedIngestionHistory> DataSeedIngestionHistories => Set<DataSeedIngestionHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Import shared canonical entities (from BuildingBlocks)

        // Import module-specific entities
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IngesterReadDbContext).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }
}