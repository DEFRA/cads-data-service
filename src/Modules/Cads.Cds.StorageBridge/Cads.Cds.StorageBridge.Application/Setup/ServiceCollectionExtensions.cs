using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.StorageBridge.Application.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorageBridgeApplicationLayer(this IServiceCollection services)
    {
        return services;
    }
}