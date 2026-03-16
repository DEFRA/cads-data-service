using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public class MiUserReportPermissionRepository(CadsReadOnlyDbContext dbContext)
    : ReadOnlyRepository<MiUserReportPermission>(dbContext), IMiUserReportPermissionRepository
{
    public async Task<IEnumerable<MiUserReportPermission>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.UserReportPermissions.Where(urp => urp.UserId == userId).ToListAsync(cancellationToken);
    }
}