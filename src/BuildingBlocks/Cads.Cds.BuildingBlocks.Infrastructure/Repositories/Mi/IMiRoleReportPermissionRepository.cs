using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public interface IMiRoleReportPermissionRepository : IReadOnlyRepository<MiRoleReportPermission>
{
    Task<IEnumerable<MiRoleReportPermission>> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default);
}