using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Application.Reports.Routing;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

[ReportHandler("holding_summary", typeof(GetHoldingSummaryRequest))]
public class GetHoldingSummaryHandler : ReportHandlerBase<GetHoldingSummaryRequest, GetHoldingSummaryQuery, JsonReportResult>
{
    public override GetHoldingSummaryQuery BuildQuery(GetHoldingSummaryRequest request)
    {
        return new GetHoldingSummaryQuery(
            request.ReportKey
        );
    }
}
