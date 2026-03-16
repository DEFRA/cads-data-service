using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public class MiUserRepository(CadsReadOnlyDbContext dbContext)
    : ReadOnlyRepository<MiUser>(dbContext), IMiUserRepository
{
    public async Task<MiUser?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Users.SingleOrDefaultAsync(u => u.UserId == userId, cancellationToken);
    }
}