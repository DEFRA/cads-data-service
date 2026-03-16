using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi
{
    public interface IMiUserRepository : IReadOnlyRepository<MiUser>
    {
        Task<MiUser?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}