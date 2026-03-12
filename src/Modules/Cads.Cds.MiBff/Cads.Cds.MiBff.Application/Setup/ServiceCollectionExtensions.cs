using Cads.Cds.MiBff.Application.Queries.Amsl2.AnimalSummary.Adapters;
using Cads.Cds.MiBff.Application.Queries.Amsl2.AnnualInventory.Adapters;
using Cads.Cds.MiBff.Application.Queries.Amsl2.DepartureDetails.Adapters;
using Cads.Cds.MiBff.Application.Queries.Amsl2.DestinationDetails.Adapters;
using Cads.Cds.MiBff.Application.Queries.Amsl2.DetailedMovements.Adapters;
using Cads.Cds.MiBff.Application.Queries.Amsl2.MovementsInSuspense.Adapters;
using Cads.Cds.MiBff.Application.Queries.Amsl2.SummaryPremiseDetails.Adapters;
using Cads.Cds.MiBff.Application.Queries.Ukv.Animals.Adapters;
using Cads.Cds.MiBff.Application.Queries.Ukv.Audit.Adapters;
using Cads.Cds.MiBff.Application.Queries.Ukv.Cohorts.Adapters;
using Cads.Cds.MiBff.Application.Queries.Ukv.DataQuality.Adapters;
using Cads.Cds.MiBff.Application.Queries.Ukv.Holdings.Adapters;
using Cads.Cds.MiBff.Application.Queries.Ukv.Inspections.Adapters;
using Cads.Cds.MiBff.Application.Queries.Ukv.JourneyHauliers.Adapters;
using Cads.Cds.MiBff.Application.Queries.Ukv.Movements.Adapters;
using Cads.Cds.MiBff.Application.Queries.Ukv.Zones.Adapters;
using Cads.Cds.MiBff.Application.Services;
using Cads.Cds.MiBff.Application.Services.Amsl2;
using Cads.Cds.MiBff.Application.Services.Ukv;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Core.Services;
using Cads.Cds.MiBff.Core.Services.Amsl2;
using Cads.Cds.MiBff.Core.Services.Ukv;
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
        // Amsl2 Adapters
        services.AddScoped<AnnualSummaryQueryAdapter>();
        services.AddScoped<AnnualInventoryQueryAdapter>();
        services.AddScoped<DepartureDetailsQueryAdapter>();
        services.AddScoped<DestinationDetailsQueryAdapter>();
        services.AddScoped<DetailedMovementsQueryAdapter>();
        services.AddScoped<MovementsInSuspenseQueryAdapter>();
        services.AddScoped<SummaryPremiseDetailsQueryAdapter>();

        // Ukv Adapters
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
        // Amsl2 Services
        services.AddTransient<IAnimalSummaryService, AnimalSummaryService>();
        services.AddTransient<IAnnualInventoryService, AnnualInventoryService>();
        services.AddTransient<IDepartureDetailsService, DepartureDetailsService>();
        services.AddTransient<IDestinationDetailsService, DestinationDetailsService>();
        services.AddTransient<IDetailedMovementsService, DetailedMovementsService>();
        services.AddTransient<IMovementsInSuspenseService, MovementsInSuspenseService>();
        services.AddTransient<ISummaryPremiseDetailsService, SummaryPremiseDetailsService>();

        // Ukv Sevices
        services.AddTransient<IAnimalService, AnimalService>();
        services.AddTransient<IAuditService, AuditService>();
        services.AddTransient<ICohortService, CohortService>();
        services.AddTransient<IDataQualityService, DataQualityService>();
        services.AddTransient<IHoldingService, HoldingService>();
        services.AddTransient<IInspectionService, InspectionService>();
        services.AddTransient<IJourneyHaulierService, JourneyHaulierService>();
        services.AddTransient<IMovementService, MovementService>();
        services.AddTransient<IZoneService, ZoneService>();

        services.AddTransient<IReportService, ReportService>();
    }
}