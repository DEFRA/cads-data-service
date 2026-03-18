using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiUserRoleRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiUserRole, MiBffReadDbContext>(dbContext), IMiUserRoleRepository
{
    public async Task<IEnumerable<MiUserRole>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.UserRoles.Where(ur => ur.UserId == userId).ToListAsync(cancellationToken);
    }
}