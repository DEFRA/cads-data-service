using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories;

public interface IMiEffectiveReportPermissionRepository : IReadOnlyRepository<MiEffectiveReportPermissionView>
{
    Task<IReadOnlyList<MiEffectiveReportPermissionView>> GetActiveByExternalSubjectAsync(
        string externalSubject,
        CancellationToken cancellationToken = default);

    Task<bool> HasReportAccessAsync(
        string externalSubject,
        string reportKey,
        CancellationToken cancellationToken = default);
}