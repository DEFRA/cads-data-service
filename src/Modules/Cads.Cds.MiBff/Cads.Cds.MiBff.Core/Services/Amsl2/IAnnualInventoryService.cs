using Cads.Cds.MiBff.Core.Domain.DTOs.Amls2;

namespace Cads.Cds.MiBff.Core.Services.Amsl2;

public interface IAnnualInventoryService
{
    Task<IEnumerable<Amsl2Dto>> GetAllAsync(CancellationToken cancellationToken = default);
}