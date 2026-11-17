namespace Cads.Cds.BuildingBlocks.Application.Imports.Envelope;

/// <summary>
/// The shape of one <c>cts_transactions</c> table as a vendor extract carries it.
/// </summary>
/// <param name="TableName">The table name, e.g. <c>ct_locations</c>.</param>
/// <param name="Prefix">The 3-character table prefix, upper-case, e.g. <c>LOC</c>.</param>
/// <param name="BusinessColumns">
/// The store column names of the business columns, lower-case, business key first and then in DDL order.
/// Pipeline columns (see <see cref="PipelineColumns"/>) are never included.
/// </param>
public sealed record EnvelopeTable(string TableName, string Prefix, IReadOnlyList<string> BusinessColumns)
{
    public string BusinessKey => BusinessColumns[0];
}