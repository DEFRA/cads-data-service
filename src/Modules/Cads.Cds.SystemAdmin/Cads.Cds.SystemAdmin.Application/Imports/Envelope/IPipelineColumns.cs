namespace Cads.Cds.BuildingBlocks.Application.Imports.Envelope;

/// <summary>
/// The pipeline and bookkeeping columns of a <c>cts_transactions.&lt;table&gt;</c> that are never part of a
/// vendor extract's business columns.
/// </summary>
public interface IPipelineColumns
{
    /// <summary>The three <c>&lt;prefix&gt;_aud_*</c> column names for a table prefix, in envelope order.</summary>
    IReadOnlyList<string> AudColumns(string prefix);

    /// <summary>True when <paramref name="column"/> is a pipeline column for a table with <paramref name="prefix"/>.</summary>
    bool IsExcluded(string column, string prefix);
}