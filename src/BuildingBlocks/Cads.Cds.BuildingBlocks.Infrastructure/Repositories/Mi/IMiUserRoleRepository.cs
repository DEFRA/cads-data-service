using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public interface IMiUserRoleRepository : IReadOnlyRepository<MiUserRole>
{
    Task<IEnumerable<MiUserRole>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}