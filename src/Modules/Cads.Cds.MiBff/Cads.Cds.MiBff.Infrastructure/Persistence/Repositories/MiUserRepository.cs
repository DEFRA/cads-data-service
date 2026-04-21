using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiUserRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiUser, MiBffReadDbContext>(dbContext), IMiUserRepository
{
    public async Task<MiUser?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await Query().SingleOrDefaultAsync(u => u.UserId == userId, cancellationToken);
    }
}