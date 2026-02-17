using Cads.Cds.Ingester.Infrastructure.Messaging.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Ingester.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration config)
    {
        services.AddMessagingDependencies(config);
    }
}