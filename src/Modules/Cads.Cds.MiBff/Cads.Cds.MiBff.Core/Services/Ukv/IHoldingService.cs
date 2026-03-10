using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Core.Services.Ukv;

public interface IHoldingService
{
    Task<IEnumerable<HoldingDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<HoldingDto>> GetByCphAsync(string cph, CancellationToken cancellationToken = default);
}