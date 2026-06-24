using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;

[ExcludeFromCodeCoverage]
public static class DbContextFactory
{
    public static T CreateInMemoryDbContext<T>(string dbName)
       where T : CadsDbContext
    {
        var options = new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase(databaseName: dbName) // Unique name per test to isolate data
            .Options;

        var instance = Activator.CreateInstance(typeof(T), options);
        if (instance is T ctx)
        {
            return ctx;
        }

        throw new InvalidOperationException(
            $"Could not create an instance of {typeof(T)}. " +
            "Ensure the type has a constructor accepting DbContextOptions or DbContextOptions<T>."
        );
    }

    public static U CreateInMemoryTestDbContextFromDbContext<T, U>(string dbName)
        where T : CadsDbContext
        where U : T
    {
        var options = new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase(databaseName: dbName) // Unique name per test to isolate data
            .Options;
        var instance = Activator.CreateInstance(typeof(U), options);
        if (instance is U ctx)
        {
            return ctx;
        }

        throw new InvalidOperationException(
            $"Could not create an instance of {typeof(U)}. " +
            "Ensure the type has a constructor accepting DbContextOptions or DbContextOptions<T>."
        );
    }
}

[ExcludeFromCodeCoverage]
public class DbContextFactory<TReadDbContext, TWriteDbContext>(IServiceProvider provider)
    : IDbContextFactory<TReadDbContext, TWriteDbContext>
    where TReadDbContext : CadsDbContext
    where TWriteDbContext : CadsDbContext
{
    public TReadDbContext CreateReadContext()
        => provider.GetRequiredService<TReadDbContext>();

    public TWriteDbContext CreateWriteContext()
        => provider.GetRequiredService<TWriteDbContext>();
}