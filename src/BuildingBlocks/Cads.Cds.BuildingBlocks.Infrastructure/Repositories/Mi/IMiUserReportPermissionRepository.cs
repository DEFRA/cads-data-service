using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public interface IMiUserReportPermissionRepository : IReadOnlyRepository<MiUserReportPermission>
{
    Task<IEnumerable<MiUserReportPermission>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}