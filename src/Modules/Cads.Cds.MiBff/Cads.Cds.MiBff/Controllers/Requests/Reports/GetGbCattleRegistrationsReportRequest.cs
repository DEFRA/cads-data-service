namespace Cads.Cds.MiBff.Controllers.Requests.Reports;

public class GetGbCattleRegistrationsReportRequest : GetReportRequest
{
    public const string ExpectedKey = "gb_cattle_registrations";

    public int Year { get; set; }
    public int Month { get; set; }
}