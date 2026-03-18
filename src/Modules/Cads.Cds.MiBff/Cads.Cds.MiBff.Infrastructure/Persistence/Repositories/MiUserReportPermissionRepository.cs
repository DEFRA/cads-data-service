using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiUserReportPermissionRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiUserReportPermission, MiBffReadDbContext>(dbContext), IMiUserReportPermissionRepository
{
    public async Task<IEnumerable<MiUserReportPermission>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.UserReportPermissions.Where(urp => urp.UserId == userId).ToListAsync(cancellationToken);
    }
}