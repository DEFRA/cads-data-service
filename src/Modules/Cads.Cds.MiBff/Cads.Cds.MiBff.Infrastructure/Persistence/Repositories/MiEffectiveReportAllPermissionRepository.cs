using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories
{
    public class MiEffectiveReportAllPermissionRepository(MiBffReadDbContext dbContext)
        : EFReadOnlyRepository<MiEffectiveReportAllPermission, MiBffReadDbContext>(dbContext), IMiEffectiveReportAllPermissionRepository
    {
        public async Task<IReadOnlyList<MiEffectiveReportAllPermission>> GetUserReportPermissionsAsync(
            string externalSubject,
            string reportKey,
            CancellationToken cancellationToken = default)
        {
            externalSubject = externalSubject.ToLower();
            return await DbContext.GetMiEffectiveReportAllPermission(externalSubject, reportKey)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}