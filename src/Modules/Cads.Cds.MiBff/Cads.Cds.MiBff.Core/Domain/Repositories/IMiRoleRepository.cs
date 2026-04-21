using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories;

public interface IMiRoleRepository : IReadOnlyRepository<MiRole>
{
    Task<MiRole?> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default);
}