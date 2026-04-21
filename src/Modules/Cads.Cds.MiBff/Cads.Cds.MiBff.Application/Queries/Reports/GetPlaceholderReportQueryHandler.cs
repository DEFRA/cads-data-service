using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetPlaceholderReportQueryHandler : IQueryHandler<GetPlaceholderReportQuery, PlaceholderReportDto>
{
    public async Task<PlaceholderReportDto> Handle(GetPlaceholderReportQuery request, CancellationToken cancellationToken)
    {
        return new PlaceholderReportDto
        {
            ReportKey = request.ReportKey
        };
    }
}