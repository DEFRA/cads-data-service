using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories;

public interface IMiUserRoleRepository : IReadOnlyRepository<MiUserRole>
{
    Task<IReadOnlyList<MiUserRole>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}