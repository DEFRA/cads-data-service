using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Application.Reports.Authorisation;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetUserReportsQueryHandler(IReportAccessService service)
    : QueryHandler<GetUserReportsQuery, ReportDto>
{
    protected override Task<IEnumerable<ReportDto>> FetchAsync(GetUserReportsQuery request, CancellationToken cancellationToken)
    {
        return service.GetUserReportsAsync(request.Identifier, cancellationToken);
    }
}