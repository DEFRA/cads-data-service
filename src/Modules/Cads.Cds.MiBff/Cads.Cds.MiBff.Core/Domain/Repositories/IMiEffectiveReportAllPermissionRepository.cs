using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories;

public interface IMiEffectiveReportAllPermissionRepository : IReadOnlyRepository<MiEffectiveReportAllPermission>
{
    Task<IReadOnlyList<MiEffectiveReportAllPermission>> GetUserReportPermissionsAsync(
        string externalSubject,
        string reportKey,
        CancellationToken cancellationToken = default);
}