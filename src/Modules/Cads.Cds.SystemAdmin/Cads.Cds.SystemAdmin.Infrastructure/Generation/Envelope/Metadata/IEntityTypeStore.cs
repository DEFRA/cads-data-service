using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Envelope.Metadata;

public interface IEntityTypeStore
{
    StoreObjectIdentifier GetTable(IReadOnlyEntityType entityType);
}