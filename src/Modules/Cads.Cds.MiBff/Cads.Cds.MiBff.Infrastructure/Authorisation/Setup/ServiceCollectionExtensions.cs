using Cads.Cds.MiBff.Application.Reports.Authorisation;
using Cads.Cds.MiBff.Infrastructure.Authorisation.Reports;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Infrastructure.Authorisation.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMiAuthorisation(this IServiceCollection services)
    {
        services.AddTransient<IReportAccessService, ReportAccessService>();

        return services;
    }
}
