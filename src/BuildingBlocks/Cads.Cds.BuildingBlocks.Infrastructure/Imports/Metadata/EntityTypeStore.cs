using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Metadata;

public sealed class EntityTypeStore : IEntityTypeStore
{
    public StoreObjectIdentifier GetTable(IReadOnlyEntityType entityType)
    {
        var tableName = entityType.GetTableName()
            ?? throw new InvalidOperationException($"Entity '{entityType.DisplayName()}' is not mapped to a table.");

        return StoreObjectIdentifier.Table(tableName, entityType.GetSchema());
    }
}