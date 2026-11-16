using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Envelope;

/// <summary>
/// Derives a <c>cts_transactions</c> table's 3-character prefix (e.g. <c>ct_locations</c> → <c>LOC</c>) from the
/// single store column ending in <c>_aud_id</c>. The prefix can't reliably be derived from the table name.
/// </summary>
public interface ITablePrefixResolver
{
    string Resolve(IReadOnlyEntityType entityType);
}