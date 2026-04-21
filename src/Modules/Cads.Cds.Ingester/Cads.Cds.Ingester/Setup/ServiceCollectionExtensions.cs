using Cads.Cds.Ingester.Application.Setup;
using Cads.Cds.Ingester.Infrastructure.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Ingester.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIngesterModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddIngesterInfrastructureLayer(config);

        services.AddIngesterApplicationLayer();

        return services;
    }
}