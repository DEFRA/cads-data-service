using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Configurations.Imports;
using Cads.Cds.StorageBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;

[ExcludeFromCodeCoverage]
public class StorageBridgeReadDbContext(DbContextOptions options) : CadsDbContext(options)
{
    // Shared canonical entities
    public DbSet<FileImport> FileImports => Set<FileImport>();

    // Module-specific entities
    public DbSet<DataSeedIngestionHistory> DataSeedIngestionHistories => Set<DataSeedIngestionHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Import shared canonical entities (from BuildingBlocks)
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(FileImportConfiguration).Assembly
        );

        // Import module-specific entities
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(StorageBridgeReadDbContext).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }
}