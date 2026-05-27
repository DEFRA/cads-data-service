using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;

[ExcludeFromCodeCoverage]
public class StorageBridgeReadDbContext(DbContextOptions<StorageBridgeReadDbContext> options) : DbContext(options)
{
    // Shared canonical entities

    // Module-specific entities

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Import shared canonical entities (from BuildingBlocks)

        // Import module-specific entities
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(StorageBridgeReadDbContext).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }
}