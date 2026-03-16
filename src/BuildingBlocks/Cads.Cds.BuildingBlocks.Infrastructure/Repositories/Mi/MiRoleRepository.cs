using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public class MiRoleRepository(CadsReadOnlyDbContext dbContext)
    : ReadOnlyRepository<MiRole>(dbContext), IMiRoleRepository
{
    public async Task<MiRole?> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Roles.SingleOrDefaultAsync(r => r.RoleId == roleId, cancellationToken);
    }
}