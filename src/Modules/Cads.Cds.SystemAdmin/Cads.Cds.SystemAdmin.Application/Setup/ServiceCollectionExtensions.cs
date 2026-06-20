using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Application.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSystemAdminApplicationLayer(this IServiceCollection services)
    {
        return services;
    }
}