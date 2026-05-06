using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Application.Reports.Routing;
using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

[ReportHandler("gb_cattle_registrations", typeof(GetGbCattleRegistrationsRequest))]
public class GetGbCattleRegistrationsHandler : ReportHandlerBase<GetGbCattleRegistrationsRequest, GetGbCattleRegistrationsQuery, FileReportResult>
{
    public override GetGbCattleRegistrationsQuery BuildQuery(GetGbCattleRegistrationsRequest request)
    {
        return new GetGbCattleRegistrationsQuery(
            request.ReportKey,
            request.Year,
            request.Month
        );
    }
}
