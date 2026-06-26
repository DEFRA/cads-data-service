using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Configurations.Imports;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;

[ExcludeFromCodeCoverage]
public class SystemAdminWriteDbContext(DbContextOptions<SystemAdminWriteDbContext> options) : CadsDbContext(options)
{
    // Shared canonical entities
    public DbSet<FileImport> FileImports => Set<FileImport>();

    // Module-specific entities

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Import shared canonical entities (from BuildingBlocks)
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(FileImportConfiguration).Assembly
        );

        // Import module-specific entities
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(SystemAdminWriteDbContext).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }
}