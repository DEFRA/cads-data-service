using Cads.Cds.BuildingBlocks.Infrastructure.Database;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;

public interface IDbContextFactory<out TReadDbContext, out TWriteDbContext>
    where TReadDbContext : CadsDbContext
    where TWriteDbContext : CadsDbContext
{
    TReadDbContext CreateReadContext();
    TWriteDbContext CreateWriteContext();
}