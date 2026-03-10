using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.Ukv.Holdings.Adapters;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Holdings;

public class GetHoldingsQueryHandler(HoldingsQueryAdapter adapter)
    : PagedQueryHandler<GetHoldingsQuery, HoldingDto>
{
    protected override async Task<(IEnumerable<HoldingDto> Items, int TotalCount)> FetchAsync(GetHoldingsQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}