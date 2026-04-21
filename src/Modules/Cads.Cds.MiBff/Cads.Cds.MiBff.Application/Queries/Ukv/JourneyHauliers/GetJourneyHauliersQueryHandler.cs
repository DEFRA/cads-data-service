using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.Ukv.JourneyHauliers.Adapters;
using Cads.Cds.MiBff.Core.DTOs.Ukv;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.JourneyHauliers;

public class GetJourneyHauliersQueryHandler(JourneyHauliersQueryAdapter adapter)
    : PagedQueryHandler<GetJourneyHauliersQuery, UkvDto>
{
    protected override async Task<(IEnumerable<UkvDto> Items, int TotalCount)> FetchAsync(GetJourneyHauliersQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}