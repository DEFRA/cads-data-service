using Cads.Cds.BuildingBlocks.Core.Persistence;

namespace Cads.Cds.MiBff.Core.Domain.Repositories;

public interface IMiUserRepository : IReadOnlyRepository<MiUser>
{
    Task<MiUser?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}