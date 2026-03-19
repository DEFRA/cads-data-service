using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories;

public interface IMiEffectiveReportPermissionRepository : IReadOnlyRepository<MiEffectiveReportPermissionView>
{
    Task<IReadOnlyList<MiEffectiveReportPermissionView>> GetByUserEmailAsync(string email, CancellationToken cancellationToken = default);
}