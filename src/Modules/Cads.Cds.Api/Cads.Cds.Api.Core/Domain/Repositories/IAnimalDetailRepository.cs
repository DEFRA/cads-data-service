using Cads.Cds.Api.Core.DTOs.Bovine;

namespace Cads.Cds.Api.Core.Domain.Repositories;

public interface IAnimalDetailRepository
{
    Task<IEnumerable<AnimalDetailDto>> GetByIdentifierAsync(string identifier, CancellationToken cancellationToken = default);
}