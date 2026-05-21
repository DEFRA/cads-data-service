using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Configurations.Livestock;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;

[ExcludeFromCodeCoverage]
public class IngesterReadDbContext(DbContextOptions<IngesterReadDbContext> options) : DbContext(options)
{
    // Shared canonical entities

    // Module-specific entities

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Import shared canonical entities
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AnimalConfiguration).Assembly
        );

        // Import module-specific entities
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IngesterReadDbContext).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }
}