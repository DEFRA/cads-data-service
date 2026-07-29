using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Configurations.Imports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database;

public abstract class CadsDbContext(DbContextOptions options) : DbContext(options)
{
    // Shared canonical entities
    public DbSet<FileImport> FileImports => Set<FileImport>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(FileImportConfiguration).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }

    public virtual async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => await Database.BeginTransactionAsync(cancellationToken);

    public virtual IExecutionStrategy CreateExecutionStrategy()
        => Database.CreateExecutionStrategy();
}