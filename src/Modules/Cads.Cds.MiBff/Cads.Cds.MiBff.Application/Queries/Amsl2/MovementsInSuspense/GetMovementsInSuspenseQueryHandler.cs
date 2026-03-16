using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.Amsl2.MovementsInSuspense.Adapters;
using Cads.Cds.MiBff.Core.DTOs.Amls2;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.MovementsInSuspense;

public class GetMovementsInSuspenseQueryHandler(MovementsInSuspenseQueryAdapter adapter)
    : PagedQueryHandler<GetMovementsInSuspenseQuery, Amsl2Dto>
{
    protected override async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> FetchAsync(GetMovementsInSuspenseQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}