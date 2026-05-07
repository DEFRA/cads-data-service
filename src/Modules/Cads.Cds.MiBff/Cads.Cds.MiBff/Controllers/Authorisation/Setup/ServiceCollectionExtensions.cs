using Cads.Cds.MiBff.Controllers.Authorisation.Handlers;
using Cads.Cds.MiBff.Controllers.Authorisation.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Controllers.Authorisation.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReportAuthorizationProviders(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, ReportAccessPolicyProvider>();

        services.AddScoped<IAuthorizationHandler, ReportAccessHandler>();
        services.AddScoped<IAuthorizationHandler, DynamicReportAccessHandler>();

        return services;
    }
}