using Cads.Cds.StorageBridge.Core.DTOs;

namespace Cads.Cds.StorageBridge.Core.Services;

public interface IBulkImportCopyService
{
    Task<bool> ExecuteAsync(CreateBulkImportJobDto dto, CancellationToken cancellationToken = default);
}