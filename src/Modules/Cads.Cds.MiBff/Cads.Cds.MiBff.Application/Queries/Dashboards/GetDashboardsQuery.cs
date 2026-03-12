using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Core.DTOs;

namespace Cads.Cds.MiBff.Application.Queries.Dashboards;

public class GetDashboardsQuery : PagedQuery<DashboardListingDto>
{
    public string? UserId { get; set; }
}