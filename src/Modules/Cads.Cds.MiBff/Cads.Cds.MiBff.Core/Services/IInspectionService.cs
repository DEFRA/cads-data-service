using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Core.Services;

public interface IInspectionService
{
    Task<IEnumerable<UkvDto>> GetAllAsync(CancellationToken cancellationToken = default);
}