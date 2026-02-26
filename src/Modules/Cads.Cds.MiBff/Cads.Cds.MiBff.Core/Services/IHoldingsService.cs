using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Core.Services
{
    public interface IHoldingsService
    {
        Task<IEnumerable<HoldingDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<HoldingDTO>> GetByCphAsync(string cph, CancellationToken cancellationToken = default);
    }
}