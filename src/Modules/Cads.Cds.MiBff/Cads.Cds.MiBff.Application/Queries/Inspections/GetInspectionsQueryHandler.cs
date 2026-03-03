using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.Inspections.Adapters;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Inspections;

public class GetDataQualityUnregisteredQueryHandler(InspectionsQueryAdapter adapter)
    : PagedQueryHandler<GetInspectionsQuery, UkvDto>
{
    protected override async Task<(IEnumerable<UkvDto> Items, int TotalCount)> FetchAsync(GetInspectionsQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}