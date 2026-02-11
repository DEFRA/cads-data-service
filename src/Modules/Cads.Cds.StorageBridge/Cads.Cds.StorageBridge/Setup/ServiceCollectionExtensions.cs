using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.StorageBridge.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorageBridgeModule(this IServiceCollection services, IConfiguration config)
    {
        return services;
    }
}
