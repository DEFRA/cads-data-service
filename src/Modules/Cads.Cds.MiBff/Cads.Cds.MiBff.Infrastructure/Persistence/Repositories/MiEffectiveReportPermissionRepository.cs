using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiEffectiveReportPermissionRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiEffectiveReportPermissionView, MiBffReadDbContext>(dbContext), IMiEffectiveReportPermissionRepository
{
    public async Task<IEnumerable<MiEffectiveReportPermissionView>> GetByUserEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbContext.EffectiveReportPermissions
            .Where(p => p.Email == email)
            .ToListAsync(cancellationToken);
    }
}