using Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IMiEffectiveReportPermissionRepository, MiEffectiveReportPermissionRepository>();

        return services;
    }
}