using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.Ukv.Zones.Adapters;
using Cads.Cds.MiBff.Core.DTOs.Ukv;

namespace Cads.Cds.MiBff.Application.Queries.Ukv.Zones;

public class GetZonesQueryHandler(ZonesQueryAdapter adapter)
    : PagedQueryHandler<GetZonesQuery, UkvDto>
{
    protected override async Task<(IEnumerable<UkvDto> Items, int TotalCount)> FetchAsync(GetZonesQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}