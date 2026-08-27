using Cads.Cds.BuildingBlocks.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.S3Import.Services;

public interface IS3ToPostgresService<T>
    where T : CreateS3ImportJobDto
{
    Task<long> ExecuteAsync(T job, CancellationToken cancellationToken = default);
}