using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;

public class S3ImportJobEnqueuer<T>(
    Channel<T> channel,
    ILogger<S3ImportJobEnqueuer<T>> logger) : IS3ImportJobEnqueuer<T>
    where T : CreateS3ImportJobDto
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