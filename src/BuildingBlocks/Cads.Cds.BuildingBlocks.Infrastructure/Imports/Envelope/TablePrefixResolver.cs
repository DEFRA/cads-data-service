using Cads.Cds.BuildingBlocks.Application.Imports.Envelope;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Envelope;

public sealed class TablePrefixResolver : ITablePrefixResolver
{
    public string Resolve(IReadOnlyEntityType entityType)
    {
        ArgumentNullException.ThrowIfNull(entityType);

        var storeObject = EntityTypeStore.GetTable(entityType);

        var audIdColumns = entityType.GetProperties()
            .Select(p => p.GetColumnName(storeObject))
            .OfType<string>()
            .Where(c => c.EndsWith(PipelineColumns.AudIdSuffix, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return audIdColumns.Count switch
        {
            1 when audIdColumns[0].Length > PipelineColumns.AudIdSuffix.Length =>
                audIdColumns[0][..^PipelineColumns.AudIdSuffix.Length].ToUpperInvariant(),
            0 => throw new InvalidOperationException(
                $"Table '{storeObject.DisplayName()}' has no column ending in '{PipelineColumns.AudIdSuffix}', so its prefix can't be resolved."),
            _ => throw new InvalidOperationException(
                $"Table '{storeObject.DisplayName()}' has {audIdColumns.Count} columns ending in '{PipelineColumns.AudIdSuffix}' " +
                $"({string.Join(", ", audIdColumns)}), so its prefix is ambiguous.")
        };
    }
}
