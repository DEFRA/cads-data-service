using Cads.Cds.Api.Core.DTOs.Bovine;
using MediatR;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnCph;

public class GetAnimalsOnCphHandler(AnimalsOnCphQueryAdapter adapter)
    : IRequestHandler<GetAnimalsOnCph, AnimalCollectionDto?>
{
    public async Task<AnimalCollectionDto?> Handle(GetAnimalsOnCph request, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(request, cancellationToken);
    }
}
