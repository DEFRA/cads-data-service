using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Holdings;

public class GetHoldingByCphQueryHandler(HoldingsQueryAdapter adapter)
    : DefaultQueryHandler<GetHoldingsByCphQuery, HoldingDto>
{
    protected override async Task<(IEnumerable<HoldingDto> Items, int TotalCount)> FetchAsync(GetHoldingsByCphQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}