using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.BulkLoad.Services;

public interface IS3ToPostgresCopyService
{
    Task<bool> ExecuteAsync(CreateS3BulkLoadJobDto job, CancellationToken cancellationToken = default);
}