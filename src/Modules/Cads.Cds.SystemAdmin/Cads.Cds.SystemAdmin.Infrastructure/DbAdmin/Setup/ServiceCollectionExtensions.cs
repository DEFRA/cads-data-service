using Cads.Cds.SystemAdmin.Application.DbAdmin.Services;
using Cads.Cds.SystemAdmin.Infrastructure.DbAdmin.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.DbAdmin.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSystemAdminDbAdminLayer(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IDbAdminExecuteCommandService, DbAdminExecuteCommandService>();
        return services;
    }
}