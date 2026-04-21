using Cads.Cds.StorageBridge.Infrastructure.Storage.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.StorageBridge.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorageBridgeInfrastructureLayer(this IServiceCollection services, IConfiguration config)
    {
        services.AddStorageBridgeStorage(config);

        return services;
    }
}