using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.Animals.Adapters;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Animals;

public class GetAnimalsByAnimalsIdQueryHandler(AnimalsQueryAdapter adapter)
    : DefaultQueryHandler<GetAnimalsByAnimalIdQuery, UkvDto>
{
    protected override async Task<(IEnumerable<UkvDto> Items, int TotalCount)> FetchAsync(GetAnimalsByAnimalIdQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}