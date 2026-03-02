using Cads.Cds.Api.Application.Services;
using CoreWCF;
using CoreWCF.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Api.Application.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiApplicationLayer(this IServiceCollection services)
    {
        // Register SOAP services
        services.AddServiceModelServices();

        // Register concrete service implementations (CoreWCF requires these)
        services.AddTransient<CattleStatusService>();
        services.AddTransient<LivestockMovementsService>();

        // Register interfaces for backward compatibility
        services.AddScoped<ICattleStatusService, CattleStatusService>();
        services.AddScoped<ILivestockMovementsService, LivestockMovementsService>();

        // Register helper services
        services.AddScoped<SoapHeaderService>();

        return services;
    }
}