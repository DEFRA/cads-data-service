using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories;

public interface IMiEffectiveReportPermissionRepository : IReadOnlyRepository<MiEffectiveReportPermission>
{
    Task<IEnumerable<MiEffectiveReportPermission>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}