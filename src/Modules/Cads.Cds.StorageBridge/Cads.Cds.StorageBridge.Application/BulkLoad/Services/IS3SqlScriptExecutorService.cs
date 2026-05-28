using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Application.BulkLoad.Services;

public interface IS3SqlScriptExecutorService
{
    Task<int> ExecuteAsync(CreateS3SqlImportJobDto job, CancellationToken cancellationToken = default);
}