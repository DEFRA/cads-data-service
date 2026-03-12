using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Application.Queries.Dashboards.Adapters;
using Cads.Cds.MiBff.Application.Queries.Ukv.Cohorts.Adapters;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Dashboards;

public class GetDashboardsQueryHandler(DashboardsQueryAdapter adapter)
    : PagedQueryHandler<GetDashboardsQuery, DashboardListingDto>
{
    protected override async Task<(IEnumerable<DashboardListingDto> Items, int TotalCount)> FetchAsync(GetDashboardsQuery query, CancellationToken cancellationToken)
    {
        return await adapter.GetAsync(query, cancellationToken);
    }
}