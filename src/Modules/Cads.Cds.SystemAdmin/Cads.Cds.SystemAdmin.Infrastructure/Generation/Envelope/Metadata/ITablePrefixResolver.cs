using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Envelope.Metadata;

public interface ITablePrefixResolver
{
    string Resolve(IReadOnlyEntityType entityType);
}