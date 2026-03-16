using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public class MiRoleReportPermissionRepository(CadsReadOnlyDbContext dbContext)
    : ReadOnlyRepository<MiRoleReportPermission>(dbContext), IMiRoleReportPermissionRepository
{
    public async Task<IEnumerable<MiRoleReportPermission>> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        return await DbContext.RoleReportPermissions.Where(r => r.RoleId == roleId).ToListAsync(cancellationToken);
    }
}