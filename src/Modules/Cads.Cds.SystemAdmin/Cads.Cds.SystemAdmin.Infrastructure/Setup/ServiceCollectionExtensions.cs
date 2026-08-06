using Cads.Cds.SystemAdmin.Infrastructure.Messaging.Setup;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSystemAdminInfrastructureLayer(this IServiceCollection services, IConfiguration config)
    {
        services.ConfigureSystemAdminPersistence();

        services.AddSystemAdminMessaging(config);

        return services;
    }
}