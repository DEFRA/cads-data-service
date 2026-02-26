using Cads.Cds.Api.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;

namespace Cads.Cds.Api.Application.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiApplicationLayer(this IServiceCollection services)
    {
        // Register SOAP services
        services.AddSoapCore();
        services.AddScoped<ICattleStatusService, CattleStatusService>();
        services.AddScoped<SoapHeaderService>();

        return services;
    }
}