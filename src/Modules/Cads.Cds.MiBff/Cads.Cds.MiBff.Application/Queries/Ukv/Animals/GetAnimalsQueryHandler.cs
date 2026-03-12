using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.Ukv.Animals.Adapters;
using Cads.Cds.MiBff.Core.DTOs.Ukv;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Animals;

public class GetAnimalsQueryHandler(AnimalsQueryAdapter adapter)
    : PagedQueryHandler<GetAnimalsQuery, UkvDto>
{
    protected override async Task<(IEnumerable<UkvDto> Items, int TotalCount)> FetchAsync(GetAnimalsQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}