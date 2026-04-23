using Cads.Cds.BuildingBlocks.Infrastructure.Database.Factories;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Setup;
using Cads.Cds.StorageBridge.Core.Services;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.Services;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.StorageBridge.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorageBridgeInfrastructureLayer(this IServiceCollection services, IConfiguration config)
    {
        services.AddPostgresDbContext<StorageBridgeWriteDbContext>();
        services.AddPostgresDbContext<StorageBridgeReadDbContext>(PostgresDataSourceFactory.ReadOnlyConnectionIdentifier);

        services.AddStorageBridgeStorage(config);
        services.RegisterServices();

        return services;
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IBulkImportCopyService, BulkImportCopyService>();
    }
}