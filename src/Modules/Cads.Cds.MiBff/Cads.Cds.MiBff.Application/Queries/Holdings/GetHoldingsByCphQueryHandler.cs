using Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;
using Cads.Cds.MiBff.Application.Queries.Pagination;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Holdings;

// Update the generic parameter to match the signature of GetHoldingsByCphQuery
public class GetHoldingByCphQueryHandler(HoldingsQueryByCphAdapter adapter) : NonPagedQueryHandler<GetHoldingsByCphQuery, HoldingDTO>
{
    private readonly HoldingsQueryByCphAdapter _adapter = adapter;

    protected override async Task<(List<HoldingDTO> Items, int TotalCount)> FetchAsync(GetHoldingsByCphQuery query, CancellationToken cancellationToken)
    {
        return await _adapter.GetHoldingsAsync(query, cancellationToken);
    }
}