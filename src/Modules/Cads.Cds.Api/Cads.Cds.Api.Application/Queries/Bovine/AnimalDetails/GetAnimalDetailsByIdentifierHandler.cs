using Cads.Cds.Api.Application.DTOs.Bovine.Animals;
using Cads.Cds.Api.Application.Queries.Bovine.Adapters;
using MediatR;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;

public class GetAnimalDetailsByIdentifierHandler(AnimalDetailsQueryAdapter adapter)
    : IRequestHandler<GetAnimalDetailsByIdentifier, AnimalDetailsDto?>
{
    public async Task<AnimalDetailsDto?> Handle(GetAnimalDetailsByIdentifier request, CancellationToken cancellationToken)
    {
        return await adapter.GetByIdentifierAsync(request, cancellationToken);
    }
}