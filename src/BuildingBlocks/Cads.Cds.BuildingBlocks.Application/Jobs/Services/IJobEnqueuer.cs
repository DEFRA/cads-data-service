using Cads.Cds.BuildingBlocks.Core.DTOs;

namespace Cads.Cds.BuildingBlocks.Application.Jobs.Services;

public interface IJobEnqueuer<T>
    where T : IQueueJob
{
    Task<Guid> EnqueueAsync(T job, CancellationToken cancellationToken = default);
}