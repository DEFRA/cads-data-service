using Cads.Cds.BuildingBlocks.Core.DTOs;

namespace Cads.Cds.BuildingBlocks.Application.Imports.Services;

public interface IS3ImportJobEnqueuer<T>
    where T : CreateS3ImportJobDto
{
    Task<Guid> EnqueueAsync(T job, CancellationToken cancellationToken = default);
}