using Microsoft.EntityFrameworkCore.Metadata;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Imports.Metadata;

public interface IBusinessColumnResolver
{
    IReadOnlyList<BusinessColumn> Resolve(IReadOnlyEntityType entityType);
    IReadOnlyList<BusinessColumn> Resolve(IReadOnlyEntityType entityType, string businessKey);
}