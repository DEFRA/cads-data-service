namespace Cads.Cds.MiBff.Application.Reports.Storage;

/// <summary>
/// Builds storage keys for generated reports, in the form
/// <c>mibff-reports/{report}/{reportJobId}</c>, e.g. <c>mibff-reports/gbcattledeaths/3f2b…</c>.
/// The report segment comes from the query type name with the "Get" prefix and "Query" suffix
/// removed, lower-cased (GetGbCattleDeathsQuery becomes gbcattledeaths).
/// </summary>
public static class ReportStorageKeys
{
    public const string Prefix = "mibff-reports";

    private const string QueryPrefix = "Get";
    private const string QuerySuffix = "Query";

    public static string For(Type queryType, Guid reportJobId)
    {
        ArgumentNullException.ThrowIfNull(queryType);

        return $"{Prefix}/{ReportSegment(queryType)}/{reportJobId}";
    }

    public static string ReportSegment(Type queryType)
    {
        ArgumentNullException.ThrowIfNull(queryType);

        var name = queryType.Name;

        if (name.StartsWith(QueryPrefix, StringComparison.Ordinal) && name.Length > QueryPrefix.Length)
            name = name[QueryPrefix.Length..];

        if (name.EndsWith(QuerySuffix, StringComparison.Ordinal) && name.Length > QuerySuffix.Length)
            name = name[..^QuerySuffix.Length];

        return name.ToLowerInvariant();
    }
}
