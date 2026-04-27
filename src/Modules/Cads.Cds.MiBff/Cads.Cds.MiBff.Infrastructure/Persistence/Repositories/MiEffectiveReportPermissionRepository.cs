using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiEffectiveReportPermissionRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiEffectiveReportPermissionView, MiBffReadDbContext>(dbContext), IMiEffectiveReportPermissionRepository
{
    public async Task<IReadOnlyList<MiEffectiveReportPermissionView>> GetActiveByExternalSubjectAsync(
        string externalSubject,
        CancellationToken cancellationToken = default)
    {
        externalSubject = externalSubject.ToUpper();
        return await Query()
            .Where(p => p.ExternalSubject.Equals(externalSubject, StringComparison.InvariantCultureIgnoreCase) &&
                        p.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> HasReportAccessAsync(
        string externalSubject,
        string reportKey,
        CancellationToken cancellationToken = default)
    {
        externalSubject = externalSubject.ToUpper();
        return await Query()
            .Where(p => p.ExternalSubject.Equals(externalSubject, StringComparison.InvariantCultureIgnoreCase) &&
                        p.ReportKey == reportKey &&
                        p.IsActive)
            .AnyAsync(cancellationToken);
    }
}