using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Services
{
    public interface IHoldingsService
    {
        Task<IEnumerable<HoldingDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<HoldingDTO>> GetByCphAsync(string cph, CancellationToken cancellationToken = default);
    }
}