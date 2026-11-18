using Cads.Cds.BuildingBlocks.Core.DTOs;
using Cads.Cds.BuildingBlocks.Infrastructure.Jobs.Services;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;

public abstract class S3ImportBackgroundService<T>(
    Channel<T> channel,
    ILogger<S3ImportBackgroundService<T>> logger,
    IS3ToPostgresService<T> processor)
    : ChannelBackgroundService<T>(channel, logger)
    where T : CreateS3ImportJobDto
{
    protected override Task HandleAsync(T job, CancellationToken cancellationToken) =>
        processor.ExecuteAsync(job, cancellationToken);
}