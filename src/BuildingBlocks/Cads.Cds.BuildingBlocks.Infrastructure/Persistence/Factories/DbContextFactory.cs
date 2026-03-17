using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;

public class DbContextFactory<TReadDbContext, TWriteDbContext>
    : IDbContextFactory<TReadDbContext, TWriteDbContext>
    where TReadDbContext : DbContext
    where TWriteDbContext : DbContext
{
    private readonly IServiceProvider _provider;

    public DbContextFactory(IServiceProvider provider)
    {
        _provider = provider;
    }

    public TReadDbContext CreateReadContext()
        => _provider.GetRequiredService<TReadDbContext>();

    public TWriteDbContext CreateWriteContext()
        => _provider.GetRequiredService<TWriteDbContext>();
}