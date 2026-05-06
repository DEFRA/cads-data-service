using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetHoldingSummaryQuery(string reportKey)
    : GetReportQuery<JsonReportResult>(reportKey)
{
}