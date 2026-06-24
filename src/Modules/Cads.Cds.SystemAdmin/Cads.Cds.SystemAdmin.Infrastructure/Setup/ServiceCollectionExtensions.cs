using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSystemAdminInfrastructureLayer(this IServiceCollection services)
    {
        services.ConfigureSystemAdminPersistence();

        return services;
    }
}