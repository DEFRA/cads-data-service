using Cads.Cds.BuildingBlocks.Application.OpenXml;
using Cads.Cds.BuildingBlocks.Core.OpenXml;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Application.Reports.Definitions.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMiBffReportsDefinitions(this IServiceCollection services)
    {
        services.AddSingleton<IReportDefinitionRegistry>(sp =>
            new ReportDefinitionRegistry(
                sp,
                [
                    typeof(IMiBffApplicationMarker).Assembly
                ]));

        services.RegisterDefinitionTypes();

        return services;
    }

    private static void RegisterDefinitionTypes(this IServiceCollection services)
    {
        services.AddTransient<IReportDefinition<MiBirthSummary>, CattleRegistrationReportDefinition>();
    }
}