using Cads.Cds.Api.Application.DTOs.Bovine.Animals;
using Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;

namespace Cads.Cds.Api.Application.Queries.Bovine.Adapters;

public class AnimalDetailsQueryAdapter(IAnimalDetailReadQuery readQuery)
{
    public async Task<AnimalDetailsDto?> GetByIdentifierAsync(
        GetAnimalDetailsByIdentifier query,
        CancellationToken cancellationToken = default)
    {
        var animal = await readQuery.GetByIdentifierAsync(query.Identifier.Trim(), cancellationToken);

        return animal?.ToDto();
    }
}