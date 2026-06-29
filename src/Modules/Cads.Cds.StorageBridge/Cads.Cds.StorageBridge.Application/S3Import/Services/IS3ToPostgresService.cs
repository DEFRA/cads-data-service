using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.S3Import.Services;

public interface IS3ToPostgresService<T>
    where T : CreateS3ImportJobDto
{
    Task<int> ExecuteAsync(T job, CancellationToken cancellationToken = default);
}