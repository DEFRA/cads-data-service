using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories;

public interface IMiPermissionRepository : IReadOnlyRepository<MiPermission>
{
    Task<MiPermission?> GetByPermissionIdAsync(Guid permissionId, CancellationToken cancellationToken = default);
}