namespace Cads.Cds.BuildingBlocks.Application.Imports.Envelope;

/// <summary>
/// The pipeline and bookkeeping columns of a <c>cts_transactions.&lt;table&gt;</c> that are never part of a
/// vendor extract's business columns. They are set by the import, written explicitly by the H/C/D/T envelope,
/// or have no place in a real delivery.
/// </summary>
public sealed class PipelineColumns : IPipelineColumns
{
    public const string AudIdSuffix = "_aud_id";

    public const string AudTypeSuffix = "_aud_type";

    public const string AudDateTimeSuffix = "_aud_datetime";

    /// <summary>
    /// Columns excluded from every table, whatever its prefix.
    /// <list type="bullet">
    /// <item><c>trans_id</c>, <c>trans_type</c>, <c>row_number</c>: assigned by the import.</item>
    /// <item><c>record_type</c>, <c>record_count</c>: written by the envelope as fixed constants.</item>
    /// <item><c>imported_date</c>, <c>cts_file_import_id</c>: import bookkeeping.</item>
    /// <item><c>fake_data</c>: real files don't carry it, so its default applies.</item>
    /// </list>
    /// </summary>
    private static readonly IReadOnlySet<string> Fixed = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "trans_id",
        "trans_type",
        "row_number",
        "record_type",
        "record_count",
        "imported_date",
        "cts_file_import_id",
        "fake_data"
    };

    /// <summary>
    /// The audit column suffixes. Prefixed with the 3-character table prefix, they are real columns of every
    /// <c>cts_transactions</c> table. A delta envelope writes them at fixed positions; they never come from the entity.
    /// </summary>
    private static readonly IReadOnlyList<string> AudSuffixes = [AudIdSuffix, AudTypeSuffix, AudDateTimeSuffix];

    public IReadOnlyList<string> AudColumns(string prefix)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(prefix);

        return [.. AudSuffixes.Select(suffix => prefix.ToLowerInvariant() + suffix)];
    }

    public bool IsExcluded(string column, string prefix)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(column);

        return Fixed.Contains(column)
            || AudColumns(prefix).Contains(column, StringComparer.OrdinalIgnoreCase);
    }
}