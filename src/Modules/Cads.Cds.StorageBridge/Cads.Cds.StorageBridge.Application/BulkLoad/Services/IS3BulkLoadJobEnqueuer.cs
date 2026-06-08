using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.BulkLoad.Services;

public interface IS3BulkLoadJobEnqueuer<T>
    where T : CreateS3BulkLoadJobDto
{
    Task<Guid> EnqueueAsync(T job, CancellationToken cancellationToken = default);
}