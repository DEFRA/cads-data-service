using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories
{
    public interface IMiUserRepository : IReadOnlyRepository<MiUser>
    {
        Task<MiUser?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}