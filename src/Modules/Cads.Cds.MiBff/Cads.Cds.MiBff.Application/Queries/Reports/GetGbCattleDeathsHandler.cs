using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Application.Reports.Routing;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

[ReportHandler("gb_cattle_deaths", typeof(GetGbCattleDeathsRequest))]
public class GetGbCattleDeathsHandler : ReportHandlerBase<GetGbCattleDeathsRequest, GetGbCattleDeathsQuery, FileReportResult>
{
    public override GetGbCattleDeathsQuery BuildQuery(GetGbCattleDeathsRequest request)
    {
        return new GetGbCattleDeathsQuery(
            request.ReportKey,
            request.Year
        );
    }
}