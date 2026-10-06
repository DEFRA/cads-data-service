using Cads.Cds.Api.Application.Queries.Setup;
using Cads.Cds.Api.Application.Soap.ServiceContracts;
using Cads.Cds.Api.Core.Configuration;
using CoreWCF.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Api.Application.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiModuleConfiguration>(configuration.GetSection(ModuleConfigurationSection.ModuleSectionName));

        // Register SOAP services
        services.AddServiceModelServices();

        // Register concrete service implementations (CoreWCF requires these)
        services.AddTransient<AnimalCohortServiceContract>();
        services.AddTransient<AnimalDetailsServiceContract>();
        services.AddTransient<AnimalPassportAndDetailsServiceContract>();
        services.AddTransient<CattleStatusServiceContract>();
        services.AddTransient<LivestockMovementsServiceContract>();

        // Queries
        services.AddQueryAdapters();

        return services;
    }
}