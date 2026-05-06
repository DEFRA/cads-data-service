using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Application.Reports.Authorisation;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetUserReportPermissionsQueryHandler(IReportAccessService reportService)
    : IQueryHandler<GetUserReportPermissionsQuery, UserReportPermissionsDto>
{
    private readonly IReportAccessService _reportService = reportService;

    public async Task<UserReportPermissionsDto> Handle(GetUserReportPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _reportService.GetUserReportPermissionsAsync(request.ExternalSubject, request.ReportKey, cancellationToken);

        return new UserReportPermissionsDto
        {
            ReportKey = request.ReportKey,
            Permissions = [.. permissions]
        };
    }
}