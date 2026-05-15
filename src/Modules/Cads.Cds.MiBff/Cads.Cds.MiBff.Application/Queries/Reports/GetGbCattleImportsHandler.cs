using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Application.Reports.Routing;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

[ReportHandler("gb_cattle_imports", typeof(GetGbCattleImportsRequest))]
public class GetGbCattleImportsHandler : ReportHandlerBase<GetGbCattleImportsRequest, GetGbCattleImportsQuery, FileReportResult>
{
    public override GetGbCattleImportsQuery BuildQuery(GetGbCattleImportsRequest request)
    {
        return new GetGbCattleImportsQuery(
            request.ReportKey,
            request.Year
        );
    }
}