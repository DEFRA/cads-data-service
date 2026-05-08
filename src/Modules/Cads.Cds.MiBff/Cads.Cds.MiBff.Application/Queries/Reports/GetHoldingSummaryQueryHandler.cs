using Cads.Cds.BuildingBlocks.Application.Queries;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetHoldingSummaryQueryHandler : IQueryHandler<GetHoldingSummaryQuery, JsonReportResult>
{
    public async Task<JsonReportResult> Handle(GetHoldingSummaryQuery query, CancellationToken cancellationToken)
    {
        var data = new List<string> { "ABC", "DEF", "GHI" };

        return new JsonReportResult(data);
    }
}