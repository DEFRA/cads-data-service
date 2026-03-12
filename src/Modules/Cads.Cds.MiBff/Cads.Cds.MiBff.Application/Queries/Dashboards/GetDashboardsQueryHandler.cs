using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.MiBff.Core.DTOs;
using Cads.Cds.MiBff.Core.Services;

namespace Cads.Cds.MiBff.Application.Queries.Dashboards;

public class GetDashboardsQueryHandler(IReportService service)
    : QueryHandler<GetUserReportListQuery, ReportListingDto>
{
    protected override Task<IEnumerable<ReportListingDto>> FetchAsync(GetUserReportListQuery request, CancellationToken cancellationToken)
    {
        return service.GetUserReportListAsync(request.UserId, cancellationToken);
    }
}