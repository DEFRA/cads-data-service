using Cads.Cds.StorageBridge.Core.Domain.Entities.Ct;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;

[ExcludeFromCodeCoverage]
public class StorageBridgeWriteDbContext(DbContextOptions<StorageBridgeWriteDbContext> options) : DbContext(options)
{
    // Shared canonical entities

    // Module-specific entities
    public DbSet<CtLocation> CtLocations => Set<CtLocation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Import shared canonical entities

        // Import module-specific entities
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(StorageBridgeWriteDbContext).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }
}