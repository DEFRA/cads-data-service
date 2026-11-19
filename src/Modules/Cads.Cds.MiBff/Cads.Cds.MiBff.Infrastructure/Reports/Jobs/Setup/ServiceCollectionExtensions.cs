using Cads.Cds.BuildingBlocks.Application.Jobs.Services;
using Cads.Cds.BuildingBlocks.Infrastructure.Jobs.Services;
using Cads.Cds.MiBff.Application.Reports.Jobs;
using Cads.Cds.MiBff.Infrastructure.Reports.Jobs.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;

namespace Cads.Cds.MiBff.Infrastructure.Reports.Jobs.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMiBffReportJobProcessing(this IServiceCollection services)
    {
        services.AddSingleton(Channel.CreateUnbounded<ProcessReportJobDto>(new UnboundedChannelOptions { SingleReader = false }));

        services.AddHostedService<ReportJobBackgroundService>();

        services.AddTransient<IJobEnqueuer<ProcessReportJobDto>, JobEnqueuer<ProcessReportJobDto>>();

        return services;
    }
}