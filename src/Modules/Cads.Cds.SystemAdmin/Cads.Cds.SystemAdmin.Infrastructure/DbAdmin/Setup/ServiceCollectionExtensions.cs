using Cads.Cds.SystemAdmin.Application.DbAdmin.Services;
using Cads.Cds.SystemAdmin.Infrastructure.DbAdmin.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.DbAdmin.Setup;

public static class ServiceCollectionExtensions
{
    public static void AddSystemAdminDbAdminLayer(this IServiceCollection services)
    {
        services.AddScoped<IDbAdminExecuteCommandService, DbAdminExecuteCommandService>();

        services.AddSingleton<CtsImportPrepareJobQueue>();
        services.AddSingleton<ICtsImportPrepareJobQueue>(sp => sp.GetRequiredService<CtsImportPrepareJobQueue>());
        services.AddScoped<ICtsImportPrepareService, CtsImportPrepareService>();
        services.AddHostedService<CtsImportPrepareBackgroundService>();
    }
}