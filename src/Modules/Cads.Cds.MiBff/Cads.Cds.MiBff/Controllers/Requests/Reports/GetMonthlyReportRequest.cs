namespace Cads.Cds.MiBff.Controllers.Requests.Reports;

public class GetMonthlyReportRequest : GetReportRequest
{
    public int Year { get; set; }
    public int Month { get; set; }
}