using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Factories;
using Cads.Cds.StorageBridge.Application.Imports.Repositories;
using Cads.Cds.StorageBridge.Application.Uow;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Behaviours;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Repositories;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Uow;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureStorageBridgePersistence(this IServiceCollection services)
    {
        services.RegisterDbContexts();

        services.RegisterBehaviours();

        services.RegisterManualUnitOfWork();

        services.RegisterRepositories();

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

    private static void RegisterManualUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IStorageBridgeUnitOfWork, StorageBridgeUnitOfWork>();
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDataSeedIngestionHistoryRepository, DataSeedIngestionHistoryRepository>();
        services.AddScoped<IStorageBridgeFileImportRepository, StorageBridgeFileImportRepository>();
    }
}