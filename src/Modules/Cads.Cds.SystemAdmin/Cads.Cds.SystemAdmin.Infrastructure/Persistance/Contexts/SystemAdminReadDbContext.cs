using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;

[ExcludeFromCodeCoverage]
public class SystemAdminReadDbContext(DbContextOptions<SystemAdminReadDbContext> options) : DbContext(options)
{
    // Shared canonical entities

    // Module-specific entities

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Import shared canonical entities (from BuildingBlocks)

        // Import module-specific entities
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(SystemAdminReadDbContext).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }
}