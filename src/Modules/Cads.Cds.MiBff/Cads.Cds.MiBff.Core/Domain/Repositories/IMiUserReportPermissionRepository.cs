using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories;

public interface IMiUserReportPermissionRepository : IReadOnlyRepository<MiUserReportPermission>
{
    Task<IEnumerable<MiUserReportPermission>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}