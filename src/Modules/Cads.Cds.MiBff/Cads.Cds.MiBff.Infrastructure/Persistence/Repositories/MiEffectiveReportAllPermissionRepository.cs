using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories
{
    public class MiEffectiveReportAllPermissionRepository(MiBffReadDbContext dbContext)
        : EFReadOnlyRepository<MiEffectiveReportAllPermissionView, MiBffReadDbContext>(dbContext), IMiEffectiveReportAllPermissionRepository
    {
        public async Task<IReadOnlyList<MiEffectiveReportAllPermissionView>> GetUserReportPermissionsAsync(
            string externalSubject,
            string reportKey,
            CancellationToken cancellationToken = default)
        {
            return await Query()
                .Where(p => p.ExternalSubject == externalSubject &&
                            p.ReportKey == reportKey)
                .ToListAsync(cancellationToken);
        }
    }
}