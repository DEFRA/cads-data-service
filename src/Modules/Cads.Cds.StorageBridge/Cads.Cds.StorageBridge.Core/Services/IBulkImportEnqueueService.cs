using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Core.Services;

public interface IBulkImportEnqueueService
{
    Task<Guid> EnqueueAsync(CreateBulkImportJobDto job, CancellationToken cancellationToken = default);
}