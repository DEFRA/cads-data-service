using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Core.Services.Amsl2;

public interface IDepartureDetailsService
{
    Task<IEnumerable<DepartureDetailsDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<DepartureDetailsDto>> GetByCphAsync(string cph, CancellationToken cancellationToken = default);
}