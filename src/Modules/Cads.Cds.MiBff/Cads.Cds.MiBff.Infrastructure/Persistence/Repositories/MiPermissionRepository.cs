using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiPermissionRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiPermission, MiBffReadDbContext>(dbContext), IMiPermissionRepository
{
    public async Task<MiPermission?> GetByPermissionIdAsync(Guid permissionId, CancellationToken cancellationToken = default)
    {
        return await Query().SingleOrDefaultAsync(p => p.PermissionId == permissionId, cancellationToken);
    }
}