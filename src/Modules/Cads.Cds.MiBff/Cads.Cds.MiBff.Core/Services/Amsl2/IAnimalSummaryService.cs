using Cads.Cds.MiBff.Core.Domain.DTOs.Amls2;

namespace Cads.Cds.MiBff.Core.Services.Amsl2;

public interface IAnimalSummaryService
{
    Task<IEnumerable<Amsl2Dto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<Amsl2Dto>> GetByAnimalIdAsync(Guid animalId, CancellationToken cancellationToken = default);
}