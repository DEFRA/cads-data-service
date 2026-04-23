using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Core.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Application.Services;

public class BulkImportEnqueueService(
    Channel<CreateBulkImportJobDto> channel,
    ILogger<BulkImportBackgroundService> logger) : IBulkImportEnqueueService
{
    public async Task<Guid> EnqueueAsync(CreateBulkImportJobDto job, CancellationToken cancellationToken = default)
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