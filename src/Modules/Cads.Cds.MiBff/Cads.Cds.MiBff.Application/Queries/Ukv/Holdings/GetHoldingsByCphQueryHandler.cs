using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
using Cads.Cds.MiBff.Application.Queries.Ukv.Holdings.Adapters;
using Cads.Cds.MiBff.Core.Domain.DTOs.Ukv;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Holdings;

public class GetHoldingByCphQueryHandler(HoldingsQueryAdapter adapter)
    : JsonResponseDataQueryHandler<GetHoldingsByCphQuery, HoldingDto>
{
    protected override async Task<(IEnumerable<HoldingDto> Items, int TotalCount)> FetchAsync(GetHoldingsByCphQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}