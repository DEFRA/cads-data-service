using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetGbCattleImportsQuery(string reportKey, int year)
    : GetReportQuery<FileReportResult>(reportKey)
{
    public const string ExpectedKey = "gb_cattle_imports";

    public int Year { get; init; } = year;
}