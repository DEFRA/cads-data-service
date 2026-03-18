using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiRoleReportPermissionRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiRoleReportPermission, MiBffReadDbContext>(dbContext), IMiRoleReportPermissionRepository
{
    public async Task<IEnumerable<MiRoleReportPermission>> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        return await DbContext.RoleReportPermissions.Where(r => r.RoleId == roleId).ToListAsync(cancellationToken);
    }
}