using Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;
using Cads.Cds.MiBff.Application.Queries.Pagination;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Holdings;

public class GetHoldingsQueryHandler(HoldingsQueryAdapter adapter)
    : PagedQueryHandler<GetHoldingsQuery, HoldingDTO>
{
    private readonly HoldingsQueryAdapter _adapter = adapter;

    protected override async Task<(List<HoldingDTO> Items, int TotalCount)> FetchAsync(GetHoldingsQuery query, CancellationToken cancellationToken)
    {
        return await _adapter.GetHoldingsAsync(query, cancellationToken);
    }
}