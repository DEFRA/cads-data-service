using Cads.Cds.Ingester.Infrastructure.Messaging.Setup;
using Cads.Cds.Ingester.Infrastructure.Storage.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Ingester.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIngesterInfrastructureLayer(this IServiceCollection services, IConfiguration config)
    {
        services.AddIngesterMessaging(config);

        services.AddIngesterStorage(config);

        return services;
    }
}