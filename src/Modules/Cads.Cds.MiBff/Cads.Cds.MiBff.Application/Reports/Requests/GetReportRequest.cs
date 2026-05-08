using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Application.Reports.Requests;

public class GetReportRequest
{
    [FromRoute(Name = "reportKey")]
    public string ReportKey { get; set; } = string.Empty;
}