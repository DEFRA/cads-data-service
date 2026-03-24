using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using Cads.Cds.MiBff.Core.Services.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetUserReportsByEmailQueryHandler(IReportService service)
    : QueryHandler<GetUserReportsByEmailQuery, ReportDto>
{
    protected override Task<IEnumerable<ReportDto>> FetchAsync(GetUserReportsByEmailQuery request, CancellationToken cancellationToken)
    {
        return service.GetUserReportsByEmailAsync(request.Email, cancellationToken);
    }
}