using Cads.Cds.BuildingBlocks.Infrastructure.Jobs.Services;
using Cads.Cds.MiBff.Application.Reports.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace Cads.Cds.MiBff.Infrastructure.Reports.Jobs.Services;

public class ReportJobBackgroundService(
    Channel<ProcessReportJobDto> channel,
    IServiceScopeFactory scopeFactory,
    ILogger<ReportJobBackgroundService> logger)
    : ChannelBackgroundService<ProcessReportJobDto>(channel, logger)
{
    protected override async Task HandleAsync(ProcessReportJobDto job, CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();

        var processor = scope.ServiceProvider.GetRequiredService<IReportJobProcessor>();

        await processor.ProcessAsync(job.ReportJobId, cancellationToken);
    }
}