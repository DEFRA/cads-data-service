using Cads.Cds.MiBff.Application.Queries.Dashboards;
using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services;

namespace Cads.Cds.MiBff.Application.Queries.Cohorts.Adapters;

public class DashboardsQueryAdapter(IDashboardService service)
{
    public async Task<(IEnumerable<DashboardListingDto> Items, int TotalCount)> GetAsync(
        GetDashboardsQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await service.GetAllForUserAsync(query.UserId, cancellationToken);
        return (items, items.Count());
    }
}