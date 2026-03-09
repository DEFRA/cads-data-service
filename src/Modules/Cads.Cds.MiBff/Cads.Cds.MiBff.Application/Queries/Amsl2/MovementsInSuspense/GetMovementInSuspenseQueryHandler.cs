using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.Amsl2.MovementsInSuspense.Adapters;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Amsl2.MovementsInSuspense;

public class GetMovementInSuspenseQueryHandler(MovementsInSuspenseQueryAdapter adapter)
    : PagedQueryHandler<GetMovementInSuspenseQuery, Amsl2Dto>
{
    protected override async Task<(IEnumerable<Amsl2Dto> Items, int TotalCount)> FetchAsync(GetMovementInSuspenseQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}