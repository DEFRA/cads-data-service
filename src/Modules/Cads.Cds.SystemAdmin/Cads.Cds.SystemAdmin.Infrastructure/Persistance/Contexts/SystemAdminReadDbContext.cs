using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;

[ExcludeFromCodeCoverage]
public class SystemAdminReadDbContext(DbContextOptions<SystemAdminReadDbContext> options) : CadsDbContext(options)
{
    // Module-specific entities

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Import module-specific entities
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(SystemAdminReadDbContext).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }
}