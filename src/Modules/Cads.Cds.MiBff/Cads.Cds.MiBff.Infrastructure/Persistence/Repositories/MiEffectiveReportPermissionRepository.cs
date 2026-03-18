using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiEffectiveReportPermissionRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiEffectiveReportPermission, MiBffReadDbContext>(dbContext), IMiEffectiveReportPermissionRepository
{
    public async Task<IEnumerable<MiEffectiveReportPermission>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.EffectiveReportPermissions
            .Include(x => x.Report)
            .Include(x => x.Permission)
            .Where(p => p.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}