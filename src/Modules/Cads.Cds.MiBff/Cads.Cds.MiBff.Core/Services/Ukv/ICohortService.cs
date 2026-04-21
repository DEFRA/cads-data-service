using Cads.Cds.MiBff.Core.DTOs.Ukv;

namespace Cads.Cds.MiBff.Core.Services.Ukv;

public interface ICohortService
{
    Task<IEnumerable<UkvDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<UkvDto>> GetByAnimalIdAsync(Guid animalId, CancellationToken cancellationToken = default);
}