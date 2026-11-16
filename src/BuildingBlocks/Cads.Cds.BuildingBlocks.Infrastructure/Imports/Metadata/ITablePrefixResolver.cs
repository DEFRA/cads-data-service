using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Metadata;

public interface ITablePrefixResolver
{
    string Resolve(IReadOnlyEntityType entityType);
}