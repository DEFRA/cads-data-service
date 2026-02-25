using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Holdings;

public class GetHoldingByCphQueryHandler(HoldingsQueryByCphAdapter adapter)
    : DefaultQueryHandler<GetHoldingsByCphQuery, HoldingDTO>
{
    private readonly HoldingsQueryByCphAdapter _adapter = adapter;

    protected override async Task<(IEnumerable<HoldingDTO> Items, int TotalCount)> FetchAsync(GetHoldingsByCphQuery query, CancellationToken cancellationToken)
    {
        return await _adapter.GetHoldingsAsync(query, cancellationToken);
    }
}