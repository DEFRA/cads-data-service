using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetGbCattleDeathsQuery(string reportKey, int year)
    : GetReportQuery<FileReportResult>(reportKey)
{
    public const string ExpectedKey = "gb_cattle_deaths";

    public int Year { get; init; } = year;
}