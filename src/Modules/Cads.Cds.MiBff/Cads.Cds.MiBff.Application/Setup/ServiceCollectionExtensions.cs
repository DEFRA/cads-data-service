using Cads.Cds.MiBff.Application.Queries.Animals.Adapters;
using Cads.Cds.MiBff.Application.Queries.Audits.Adapters;
using Cads.Cds.MiBff.Application.Queries.Cohorts.Adapters;
using Cads.Cds.MiBff.Application.Queries.DataQuality.Adapters;
using Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;
using Cads.Cds.MiBff.Application.Queries.Inspections.Adapters;
using Cads.Cds.MiBff.Application.Queries.JourneyHauliers.Adapters;
using Cads.Cds.MiBff.Application.Queries.Movements.Adapters;
using Cads.Cds.MiBff.Application.Queries.Zones.Adapters;
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
        services.AddScoped<AnimalsQueryAdapter>();
        services.AddScoped<AuditQueryAdapter>();
        services.AddScoped<CohortsQueryAdapter>();
        services.AddScoped<DataQualityQueryAdapter>();
        services.AddScoped<HoldingsQueryAdapter>();
        services.AddScoped<InspectionsQueryAdapter>();
        services.AddScoped<JourneyHauliersQueryAdapter>();
        services.AddScoped<MovementsQueryAdapter>();
        services.AddScoped<ZonesQueryAdapter>();
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IAnimalService, AnimalService>();
        services.AddTransient<IAuditService, AuditService>();
        services.AddTransient<ICohortService, CohortService>();
        services.AddTransient<IDataQualityService, DataQualityService>();
        services.AddTransient<IHoldingService, HoldingService>();
        services.AddTransient<IInspectionService, InspectionService>();
        services.AddTransient<IJourneyHaulierService, JourneyHaulierService>();
        services.AddTransient<IMovementService, MovementService>();
        services.AddTransient<IZoneService, ZoneService>();
    }
}