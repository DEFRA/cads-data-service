using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiRoleRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiRole, MiBffReadDbContext>(dbContext), IMiRoleRepository
{
    public async Task<MiRole?> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Roles.SingleOrDefaultAsync(r => r.RoleId == roleId, cancellationToken);
    }
}