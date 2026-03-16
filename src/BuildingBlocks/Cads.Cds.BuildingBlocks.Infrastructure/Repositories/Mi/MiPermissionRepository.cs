using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public class MiPermissionRepository(CadsReadOnlyDbContext dbContext)
    : ReadOnlyRepository<MiPermission>(dbContext), IMiPermissionRepository
{
    public async Task<MiPermission?> GetByPermissionIdAsync(Guid permissionId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Permissions.SingleOrDefaultAsync(p => p.PermissionId == permissionId, cancellationToken);
    }
}