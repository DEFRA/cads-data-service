using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public interface IMiEffectiveReportPermissionRepository : IReadOnlyRepository<MiEffectiveReportPermission>
{
    Task<IEnumerable<MiEffectiveReportPermission>> GetByUserIdAsync(Guid reportId, CancellationToken cancellationToken = default);
}