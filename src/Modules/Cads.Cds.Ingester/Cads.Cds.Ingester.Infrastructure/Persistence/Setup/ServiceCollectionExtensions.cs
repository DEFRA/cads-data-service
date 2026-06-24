using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.Ingester.Infrastructure.Persistence.Behaviours;
using Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Ingester.Infrastructure.Persistence.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureIngesterPersistence(this IServiceCollection services)
    {
        services.RegisterDbContexts();

        services.RegisterBehaviours();

        return services;
    }

    private static void RegisterDbContexts(this IServiceCollection services)
    {
        services.AddPostgresDbContext<IngesterWriteDbContext>();
        services.AddPostgresDbContext<IngesterReadDbContext>(PostgresDataSourceFactory.ReadOnlyConnectionIdentifier);

        services.AddScoped<
            IDbContextFactory<IngesterReadDbContext, IngesterWriteDbContext>,
            DbContextFactory<IngesterReadDbContext, IngesterWriteDbContext>>();
    }

    private static void RegisterBehaviours(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(IngesterTransactionBehaviour<,>));
    }
}