using Cads.Cds.BuildingBlocks.Core.DTOs;
using Cads.Cds.StorageBridge.Application.S3Import.Model;

namespace Cads.Cds.StorageBridge.Application.S3Import.Services;

public interface IS3ToPostgresService<T>
    where T : CreateS3ImportJobDto
{
    Task<S3ToPostgresResult> ExecuteAsync(T job, CancellationToken cancellationToken = default);
}