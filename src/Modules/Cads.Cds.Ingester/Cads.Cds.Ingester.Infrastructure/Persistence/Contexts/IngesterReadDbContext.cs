using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;

[ExcludeFromCodeCoverage]
public class IngesterReadDbContext(DbContextOptions options) : CadsDbContext(options)
{
    // Shared canonical entities

    // Module-specific entities

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