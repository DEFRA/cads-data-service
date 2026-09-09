using Cads.Cds.Api.Core.Domain.Repositories;
using Cads.Cds.Api.Core.DTOs.Bovine;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;

public class AnimalDetailsQueryAdapter(IAnimalDetailRepository repository)
{
    public async Task<IEnumerable<AnimalDetailDto>> GetAsync(
        GetAnimalDetailsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await repository.GetByIdentifierAsync(query.Identifier, cancellationToken);
    }
}
