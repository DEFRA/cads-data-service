using Cads.Cds.Api.Core.Domain.Repositories;
using Cads.Cds.Api.Core.DTOs.Bovine;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;

public class AnimalDetailsQueryAdapter(IAnimalDetailRepository repository)
{
    public async Task<AnimalDetailDto?> GetAsync(
        GetAnimalDetailsByIdentifier query,
        CancellationToken cancellationToken = default)
    {
        var animals = await repository.GetByIdentifierAsync(query.Identifier, cancellationToken);

        return animals.SingleOrDefault();
    }
}