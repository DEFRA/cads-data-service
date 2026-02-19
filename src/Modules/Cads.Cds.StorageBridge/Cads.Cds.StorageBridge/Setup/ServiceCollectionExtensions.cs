using Cads.Cds.StorageBridge.Application.Setup;
using Cads.Cds.StorageBridge.Infrastructure.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.StorageBridge.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorageBridgeModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddStorageBridgeInfrastructureLayer(config);

        services.AddStorageBridgeApplicationLayer();

        return services;
    }
}