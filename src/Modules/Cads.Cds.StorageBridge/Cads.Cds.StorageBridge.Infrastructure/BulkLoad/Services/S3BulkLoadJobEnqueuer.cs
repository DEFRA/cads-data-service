using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Services;

public class S3BulkLoadJobEnqueuer<T>(
    Channel<T> channel,
    ILogger<S3BulkLoadJobEnqueuer<T>> logger) : IS3BulkLoadJobEnqueuer<T>
    where T : CreateS3BulkLoadJobDto
{
    public async Task<Guid> EnqueueAsync(T job, CancellationToken cancellationToken = default)
    {
        job.JobId = Guid.NewGuid();

        await channel.Writer.WriteAsync(job, cancellationToken);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Enqueued bulk import job with ID: {JobId}", job.JobId);
        }

        return job.JobId.Value;
    }
}