using Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cads.Cds.MiBff.Application.Reports.Routing.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMiBffReportsRouting(this IServiceCollection services)
    {
        services.AddSingleton<IReportRegistry, ReportRegistry>();

        var handlerTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => !t.IsAbstract && typeof(IReportHandler).IsAssignableFrom(t));

        foreach (var type in handlerTypes)
        {
            services.AddScoped(type);
        }

        return services;
    }
}
