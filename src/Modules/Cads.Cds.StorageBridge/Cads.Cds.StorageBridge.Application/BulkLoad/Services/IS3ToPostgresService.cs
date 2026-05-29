using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.BulkLoad.Services;

public interface IS3ToPostgresService<T>
    where T: CreateS3BulkLoadJobDto
{
    Task<bool> ExecuteAsync(T job, CancellationToken cancellationToken = default);
}