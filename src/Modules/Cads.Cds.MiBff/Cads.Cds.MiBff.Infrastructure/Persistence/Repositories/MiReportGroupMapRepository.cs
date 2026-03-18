using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiReportGroupMapRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiReportGroupMap, MiBffReadDbContext>(dbContext), IMiReportGroupMapRepository
{
    public async Task<IEnumerable<MiReportGroupMap>> GetByGroupIdAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<MiReportGroupMap>().Where(m => m.GroupId == groupId).ToListAsync(cancellationToken);
    }
}