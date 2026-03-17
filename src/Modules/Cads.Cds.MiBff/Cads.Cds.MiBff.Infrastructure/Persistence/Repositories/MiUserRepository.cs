using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiUserRepository
    : EfReadOnlyRepository<MiUser, MiBffReadDbContext>, IMiUserRepository
{
    public MiUserRepository(MiBffReadDbContext dbContext)
        : base(dbContext) { }

    public Task<MiUser?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        => Query()
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);
}