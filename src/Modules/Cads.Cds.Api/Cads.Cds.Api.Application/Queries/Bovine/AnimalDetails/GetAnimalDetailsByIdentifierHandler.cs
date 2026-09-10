using Cads.Cds.Api.Core.DTOs.Bovine;
using MediatR;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;

public class GetAnimalDetailsByIdentifierHandler(AnimalDetailsQueryAdapter adapter)
    : IRequestHandler<GetAnimalDetailsByIdentifier, AnimalDetailDto?>
{
    public async Task<AnimalDetailDto?> Handle(GetAnimalDetailsByIdentifier request, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(request, cancellationToken);
    }
}
