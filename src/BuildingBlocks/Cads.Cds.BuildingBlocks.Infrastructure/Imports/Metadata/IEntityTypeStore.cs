using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Metadata;

public interface IEntityTypeStore
{
    StoreObjectIdentifier GetTable(IReadOnlyEntityType entityType);
}
