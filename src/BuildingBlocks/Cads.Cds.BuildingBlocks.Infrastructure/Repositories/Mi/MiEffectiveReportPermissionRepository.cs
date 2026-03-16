using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public class MiEffectiveReportPermissionRepository(CadsReadOnlyDbContext dbContext)
    : ReadOnlyRepository<MiEffectiveReportPermission>(dbContext), IMiEffectiveReportPermissionRepository
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