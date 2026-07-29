using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;

[ExcludeFromCodeCoverage]
public class IngesterWriteDbContext(DbContextOptions<IngesterWriteDbContext> options) : CadsDbContext(options)
{
    // Module-specific entities

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Import module-specific entities
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IngesterWriteDbContext).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }
}