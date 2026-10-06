using Cads.Cds.Api.Application.DTOs.Bovine.Animals;
using Cads.Cds.Api.Application.Queries.Bovine.Adapters;
using MediatR;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnHolding;

public class GetAnimalsOnHoldingHandler(AnimalsOnHoldingQueryAdapter adapter)
    : IRequestHandler<GetAnimalsOnHolding, AnimalsOnHoldingDto?>
{
    public async Task<AnimalsOnHoldingDto?> Handle(GetAnimalsOnHolding request, CancellationToken cancellationToken)
    {
        return await adapter.SearchAsync(request, cancellationToken);
    }
}