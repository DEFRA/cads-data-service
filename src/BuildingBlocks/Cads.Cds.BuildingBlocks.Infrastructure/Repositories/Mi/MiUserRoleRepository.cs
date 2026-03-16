using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public class MiUserRoleRepository(CadsReadOnlyDbContext dbContext)
    : ReadOnlyRepository<MiUserRole>(dbContext), IMiUserRoleRepository
{
    public async Task<IEnumerable<MiUserRole>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.UserRoles.Where(ur => ur.UserId == userId).ToListAsync(cancellationToken);
    }
}