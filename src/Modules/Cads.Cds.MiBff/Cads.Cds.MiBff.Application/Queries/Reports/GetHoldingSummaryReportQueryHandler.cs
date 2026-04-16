using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetHoldingSummaryReportQueryHandler : IQueryHandler<GetHoldingSummaryReportQuery, HoldingSummaryReportDto>
{
    public async Task<HoldingSummaryReportDto> Handle(GetHoldingSummaryReportQuery request, CancellationToken cancellationToken)
    {
        return new HoldingSummaryReportDto { 
            ReportId = Guid.NewGuid(),
            ReportKey = request.ReportKey
        };
    }
}
