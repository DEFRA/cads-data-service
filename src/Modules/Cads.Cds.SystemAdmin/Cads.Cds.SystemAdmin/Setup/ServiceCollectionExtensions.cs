using Cads.Cds.SystemAdmin.Application.Setup;
using Cads.Cds.SystemAdmin.Infrastructure.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSystemAdminModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddSystemAdminInfrastructureLayer();

        services.AddSystemAdminApplicationLayer();

        return services;
    }
}