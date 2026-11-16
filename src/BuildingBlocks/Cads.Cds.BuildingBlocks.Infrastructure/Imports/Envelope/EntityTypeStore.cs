using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Envelope;

internal static class EntityTypeStore
{
    public static StoreObjectIdentifier GetTable(IReadOnlyEntityType entityType)
    {
        var tableName = entityType.GetTableName()
            ?? throw new InvalidOperationException($"Entity '{entityType.DisplayName()}' is not mapped to a table.");

        return StoreObjectIdentifier.Table(tableName, entityType.GetSchema());
    }
}
