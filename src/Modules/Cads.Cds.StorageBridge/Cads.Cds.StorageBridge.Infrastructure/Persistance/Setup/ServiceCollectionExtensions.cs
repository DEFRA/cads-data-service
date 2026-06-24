using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Behaviours;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureStorageBridgePersistence(this IServiceCollection services)
    {
        services.RegisterDbContexts();

        services.RegisterBehaviours();

        return services;
    }

    private static void RegisterDbContexts(this IServiceCollection services)
    {
        services.AddPostgresDbContext<StorageBridgeWriteDbContext>();
        services.AddPostgresDbContext<StorageBridgeReadDbContext>(PostgresDataSourceFactory.ReadOnlyConnectionIdentifier);

        services.AddScoped<
            IDbContextFactory<StorageBridgeReadDbContext, StorageBridgeWriteDbContext>,
            DbContextFactory<StorageBridgeReadDbContext, StorageBridgeWriteDbContext>>();
    }

    private static void RegisterBehaviours(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(StorageBridgeTransactionBehaviour<,>));
    }
}