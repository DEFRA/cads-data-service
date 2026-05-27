using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Services;

public class S3BulkLoadJobEnqueuer(
    Channel<CreateS3BulkLoadJobDto> channel,
    ILogger<S3BulkLoadJobEnqueuer> logger) : IS3BulkLoadJobEnqueuer
{
    public async Task<Guid> EnqueueAsync(CreateS3BulkLoadJobDto job, CancellationToken cancellationToken = default)
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