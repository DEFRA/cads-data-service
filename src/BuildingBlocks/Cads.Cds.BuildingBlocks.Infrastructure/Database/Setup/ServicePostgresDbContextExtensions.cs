using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;

public static class ServicePostgresDbContextExtensions
{
    public static IServiceCollection AddPostgresDbContext<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        services.AddDbContext<TContext>((sp, options) =>
        {
            var dataSourceFactory = sp.GetRequiredService<IPostgresDataSourceFactory>();
            var dataSource = dataSourceFactory.CreateDataSource(PostgresDataSourceFactory.DefaultConnectionIdentifier);
            options.UseNpgsql(dataSource);
        });
        return services;
    }

    // Overload for modules that need to specify connection identifier
    public static IServiceCollection AddPostgresDbContext<TContext>(this IServiceCollection services, string connectionIdentifier) where TContext : DbContext
    {
        services.AddDbContext<TContext>((sp, options) =>
        {
            var dataSourceFactory = sp.GetRequiredService<IPostgresDataSourceFactory>();
            var dataSource = dataSourceFactory.CreateDataSource(connectionIdentifier);
            options.UseNpgsql(dataSource);
        });
        return services;
    }
}