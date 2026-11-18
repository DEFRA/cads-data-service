using Cads.Cds.BuildingBlocks.Application.Jobs.Services;
using Cads.Cds.BuildingBlocks.Core.DTOs;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Jobs.Services;

public class JobEnqueuer<T>(
    Channel<T> channel,
    ILogger<JobEnqueuer<T>> logger) : IJobEnqueuer<T>
    where T : IQueueJob
{
    public async Task<Guid> EnqueueAsync(T job, CancellationToken cancellationToken = default)
    {
        job.JobId = Guid.NewGuid();

        await channel.Writer.WriteAsync(job, cancellationToken);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Enqueued job with ID: {JobId}", job.JobId);
        }

        return job.JobId.Value;
    }
}