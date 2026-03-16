using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public interface IMiPermissionRepository : IReadOnlyRepository<MiPermission>
{
    Task<MiPermission?> GetByPermissionIdAsync(Guid permissionId, CancellationToken cancellationToken = default);
}