using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Ingester.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIngesterModule(this IServiceCollection services, IConfiguration config)
    {
        return services;
    }
}