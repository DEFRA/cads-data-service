using Cads.Cds.SystemAdmin.Application.Imports.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Application.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSystemAdminApplicationLayer(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ImportsDeduplication>(
            config.GetSection("ImportsDeduplicationSectionName"));

        return services;
    }
}