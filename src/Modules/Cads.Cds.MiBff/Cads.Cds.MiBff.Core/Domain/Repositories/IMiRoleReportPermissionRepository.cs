using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories;

public interface IMiRoleReportPermissionRepository : IReadOnlyRepository<MiRoleReportPermission>
{
    Task<IEnumerable<MiRoleReportPermission>> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default);
}