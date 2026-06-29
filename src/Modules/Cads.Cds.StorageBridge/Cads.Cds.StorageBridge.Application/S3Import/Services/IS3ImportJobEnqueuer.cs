using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.BulkLoad.Services;

public interface IS3ImportJobEnqueuer<T>
    where T : CreateS3ImportJobDto
{
    Task<Guid> EnqueueAsync(T job, CancellationToken cancellationToken = default);
}