using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Configurations.Livestock;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;

[ExcludeFromCodeCoverage]
public class IngesterWriteDbContext(DbContextOptions<IngesterWriteDbContext> options) : DbContext(options)
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
            typeof(IngesterWriteDbContext).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }
}