using Cads.Cds.StorageBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;

[ExcludeFromCodeCoverage]
public class StorageBridgeWriteDbContext(DbContextOptions<StorageBridgeWriteDbContext> options) : DbContext(options)
{
    // Shared canonical entities

    // Module-specific entities
    public DbSet<DataSeedIngestionHistory> DataSeedIngestionHistories => Set<DataSeedIngestionHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Import shared canonical entities (from BuildingBlocks)

        // Import module-specific entities
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(StorageBridgeWriteDbContext).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }
}