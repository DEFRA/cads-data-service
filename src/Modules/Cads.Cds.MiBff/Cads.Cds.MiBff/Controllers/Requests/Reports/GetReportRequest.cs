using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers.Requests.Reports;

public class GetReportRequest
{
    [FromRoute(Name = "reportKey")]
    public string ReportKey { get; set; } = string.Empty;
}