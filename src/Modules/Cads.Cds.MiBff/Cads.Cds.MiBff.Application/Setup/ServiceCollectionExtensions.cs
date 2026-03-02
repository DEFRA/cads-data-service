using Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;
using Cads.Cds.MiBff.Application.Services;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Application.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMiBffApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MiBffModuleConfiguration>(configuration.GetSection(ModuleConfigurationSection.ModuleSectionName));

        services.RegisterAdapters();
        services.RegisterServices();

        return services;
    }

    public static void RegisterAdapters(this IServiceCollection services)
    {
        services.AddScoped<HoldingsQueryAdapter>();
        services.AddScoped<HoldingsQueryAdapter>();
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IHoldingService, HoldingService>();
    }
}