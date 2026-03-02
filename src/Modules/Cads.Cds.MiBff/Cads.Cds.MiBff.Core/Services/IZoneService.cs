using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Core.Services;

public interface IZoneService
{
    Task<IEnumerable<UkvDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<UkvDto>> GetByZoneIdAsync(Guid zoneId, CancellationToken ct = default);
}