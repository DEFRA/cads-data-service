using Cads.Cds.MiBff.Core.DTOs.Amls2;

namespace Cads.Cds.MiBff.Core.Services.Amsl2;

public interface IDepartureDetailsService
{
    Task<IEnumerable<DepartureDetailsDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<DepartureDetailsDto>> GetByCphAsync(string cph, CancellationToken cancellationToken = default);
}