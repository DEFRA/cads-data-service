using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Application.Reports.Routing;

public class ReportKeyModelRegistry : IReportKeyModelRegistry
{
    // Only gb_cattle_deaths currently stores raw json via the async job pipeline
    // Handlers for gb_cattle_registrations and gb_cattle_imports are currently synchronous and return xlsx directly, these need to be updated
    private static readonly IReadOnlyDictionary<string, Type> ModelTypesByReportKey =
        new Dictionary<string, Type>(StringComparer.Ordinal)
        {
            [GetGbCattleDeathsQuery.ExpectedKey] = typeof(MiDeathSummary),
            [GetGbCattleRegistrationsQuery.ExpectedKey] = typeof(MiBirthSummary),
            [GetGbCattleImportsQuery.ExpectedKey] = typeof(MiImportSummary),
        };

    public bool TryGetModelType(string reportKey, out Type? modelType)
        => ModelTypesByReportKey.TryGetValue(reportKey, out modelType);
}