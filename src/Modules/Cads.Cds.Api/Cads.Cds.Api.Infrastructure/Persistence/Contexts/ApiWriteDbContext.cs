using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.Api.Infrastructure.Persistence.Contexts;

[ExcludeFromCodeCoverage]
public class ApiWriteDbContext(DbContextOptions<ApiWriteDbContext> options) : CadsDbContext(options)
{
    // Shared canonical entities

    // Module-specific entities

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Import shared canonical entities (from BuildingBlocks)

        // Import module-specific entities
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApiWriteDbContext).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }
}