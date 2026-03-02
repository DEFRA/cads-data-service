using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Services;

public interface IAnimalService
{
    Task<IEnumerable<UkvDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<UkvDto>> GetByAnimalIdAsync(string animalId, CancellationToken ct = default);
}