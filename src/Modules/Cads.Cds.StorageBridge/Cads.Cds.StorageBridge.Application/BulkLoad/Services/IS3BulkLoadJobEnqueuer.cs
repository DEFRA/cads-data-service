using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.BulkLoad.Services;

public interface IS3BulkLoadJobEnqueuer
{
    Task<Guid> EnqueueAsync(CreateS3BulkLoadJobDto job, CancellationToken cancellationToken = default);
}