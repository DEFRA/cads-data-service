using Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IMiEffectiveReportPermissionRepository, MiEffectiveReportPermissionRepository>();
        services.AddScoped<IMiPermissionRepository, MiPermissionRepository>();
        services.AddScoped<IMiReportGroupRepository, MiReportGroupRepository>();
        services.AddScoped<IMiReportGroupMapRepository, MiReportGroupMapRepository>();
        services.AddScoped<IMiReportRepository, MiReportRepository>();
        services.AddScoped<IMiRoleReportPermissionRepository, MiRoleReportPermissionRepository>();
        services.AddScoped<IMiRoleRepository, MiRoleRepository>();
        services.AddScoped<IMiUserReportPermissionRepository, MiUserReportPermissionRepository>();
        services.AddScoped<IMiUserRepository, MiUserRepository>();
        services.AddScoped<IMiUserRoleRepository, MiUserRoleRepository>();
        
        return services;
    }
}