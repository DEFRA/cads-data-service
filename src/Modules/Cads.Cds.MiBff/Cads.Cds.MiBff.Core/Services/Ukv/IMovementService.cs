using Cads.Cds.MiBff.Core.Domain.DTOs.Ukv;

namespace Cads.Cds.MiBff.Core.Services.Ukv;

public interface IMovementService
{
    Task<IEnumerable<UkvDto>> GetAllAsync(CancellationToken cancellationToken = default);
}