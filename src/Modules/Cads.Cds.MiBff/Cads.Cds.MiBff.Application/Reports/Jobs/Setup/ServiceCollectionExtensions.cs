using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Application.Reports.Jobs.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMiBffReportJobs(this IServiceCollection services)
    {
        services.AddScoped<IReportJobQueryBuilder, ReportJobQueryBuilder>();
        services.AddScoped<IReportJobProcessor, ReportJobProcessor>();

        return services;
    }
}
