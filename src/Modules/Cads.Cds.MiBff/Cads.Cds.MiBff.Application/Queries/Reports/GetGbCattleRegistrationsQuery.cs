using Cads.Cds.MiBff.Core.DTOs.Reports;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetGbCattleRegistrationsQuery(string reportKey, int year, int month)
    : GetReportQuery<FileReportResult>(reportKey)
{
    public int Year { get; init; } = year;
    public int Month { get; init; } = month;
}
