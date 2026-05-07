using Cads.Cds.MiBff.Core.DTOs.Reports;
using FluentValidation;

namespace Cads.Cds.MiBff.Application.Queries.Reports;

public class GetGbCattleRegistrationsQuery(string reportKey, int year, int month)
    : GetReportQuery<FileReportResult>(reportKey)
{
    public const string ExpectedKey = "gb_cattle_registrations";

    public int Year { get; init; } = year;
    public int Month { get; init; } = month;
}