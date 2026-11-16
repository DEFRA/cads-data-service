using Cads.Cds.BuildingBlocks.Application.Imports.Envelope;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Metadata;

public sealed class TablePrefixResolver(IEntityTypeStore entityTypeStore) : ITablePrefixResolver
{
    public string Resolve(IReadOnlyEntityType entityType)
    {
        ArgumentNullException.ThrowIfNull(entityType);

        var storeObject = entityTypeStore.GetTable(entityType);

        var audIdColumns = entityType.GetProperties()
            .Select(p => p.GetColumnName(storeObject))
            .OfType<string>()
            .Where(c => c.EndsWith(PipelineColumns.AudIdSuffix, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (audIdColumns.Count > 1)
        {
            throw new InvalidOperationException($"Table '{storeObject.DisplayName()}' has {audIdColumns.Count} columns ending in '{PipelineColumns.AudIdSuffix}' ({string.Join(", ", audIdColumns)}), so its prefix is ambiguous.");
        }

        if (audIdColumns.Count == 0)
        {
            throw new InvalidOperationException($"Table '{storeObject.DisplayName()}' has no column ending in '{PipelineColumns.AudIdSuffix}', so its prefix can't be resolved.");
        }
        
        var tablePrefix = audIdColumns[0][..^PipelineColumns.AudIdSuffix.Length].ToUpperInvariant();
        
        return tablePrefix;
    }
}
