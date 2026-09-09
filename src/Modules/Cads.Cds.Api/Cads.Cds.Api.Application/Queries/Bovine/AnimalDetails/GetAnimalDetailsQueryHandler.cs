using Cads.Cds.Api.Core.DTOs.Bovine;
using Cads.Cds.BuildingBlocks.Application.Queries;

namespace Cads.Cds.Api.Application.Queries.Bovine.AnimalDetails;

public class GetAnimalDetailsQueryHandler(AnimalDetailsQueryAdapter adapter)
    : QueryHandler<GetAnimalDetailsQuery, AnimalDetailDto>
{
    protected override async Task<IEnumerable<AnimalDetailDto>> FetchAsync(GetAnimalDetailsQuery request, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(request, cancellationToken);
    }
}
