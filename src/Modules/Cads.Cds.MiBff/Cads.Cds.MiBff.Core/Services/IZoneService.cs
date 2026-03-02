using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Services;

public interface IZoneService
{
    Task<IEnumerable<UkvDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<UkvDto>> GetByZoneIdAsync(string zoneId, CancellationToken ct = default);
}