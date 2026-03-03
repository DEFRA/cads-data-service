using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.DataQuality.Adapters;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.DataQuality;

public class GetDataQualityUnregisteredQueryHandler(DataQualityQueryAdapter adapter)
    : PagedQueryHandler<GetDataQualityUnregisteredQuery, UkvDto>
{
    protected override async Task<(IEnumerable<UkvDto> Items, int TotalCount)> FetchAsync(GetDataQualityUnregisteredQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}