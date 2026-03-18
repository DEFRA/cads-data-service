using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;

public class DbContextFactory<TReadDbContext, TWriteDbContext>(IServiceProvider provider)
    : IDbContextFactory<TReadDbContext, TWriteDbContext>
    where TReadDbContext : DbContext
    where TWriteDbContext : DbContext
{
    public TReadDbContext CreateReadContext()
        => provider.GetRequiredService<TReadDbContext>();

    public TWriteDbContext CreateWriteContext()
        => provider.GetRequiredService<TWriteDbContext>();
}