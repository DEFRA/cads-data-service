using Cads.Cds.MiBff.Core.Domain.DTOs.Ukv;

namespace Cads.Cds.MiBff.Core.Services.Ukv;

public interface IZoneService
{
    Task<IEnumerable<UkvDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<UkvDto>> GetByZoneIdAsync(Guid zoneId, CancellationToken cancellationToken = default);
}