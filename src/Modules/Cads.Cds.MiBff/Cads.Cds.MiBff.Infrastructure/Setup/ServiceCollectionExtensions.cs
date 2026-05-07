using Cads.Cds.MiBff.Application.Services.Reports;
using Cads.Cds.MiBff.Infrastructure.Authorisation.Setup;
using Cads.Cds.MiBff.Infrastructure.Persistence.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMiBffInfrastructureLayer(this IServiceCollection services)
    {
        services.ConfigureMiBffPersistence();
        services.ConfigureMiAuthorisation();

        // TODO: This all needs to move
        services.AddTransient<IReportGenerationService, ReportGenerationService>();
        services.AddTransient<IOpenXmlReportGenerator, OpenXmlReportGenerator>();
        return services;
    }
}