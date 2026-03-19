using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;

public interface IDbContextFactory<out TReadDbContext, out TWriteDbContext>
    where TReadDbContext : DbContext
    where TWriteDbContext : DbContext
{
    TReadDbContext CreateReadContext();
    TWriteDbContext CreateWriteContext();
}