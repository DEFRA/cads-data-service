using Cads.Cds.MiBff.Application.Services.Reports;
using Cads.Cds.MiBff.Core.Services.Reports;
using Cads.Cds.MiBff.Infrastructure.Authorisation.Setup;
using Cads.Cds.MiBff.Infrastructure.Persistence.Setup;
using Cads.Cds.MiBff.Infrastructure.Reports;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Infrastructure.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMiBffInfrastructureLayer(this IServiceCollection services)
    {
        services.ConfigureMiBffPersistence();
        services.ConfigureMiAuthorisation();

        // TODO: This all needs to move
        services.AddTransient<IReportRepository, FakeReportRepository>();
        services.AddTransient<IReportGenerationService, ReportGenerationService>();
        services.AddTransient<IXlsxReportGenerator, XlsxReportGenerator>();

        return services;
    }
}