using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public interface IMiRoleRepository : IReadOnlyRepository<MiRole>
{
    Task<MiRole?> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default);
}