using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public class MiReportGroupMapRepository(CadsReadOnlyDbContext dbContext)
    : ReadOnlyRepository<MiReportGroupMap>(dbContext), IMiReportGroupMapRepository
{
    public async Task<IEnumerable<MiReportGroupMap>> GetByGroupIdAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<MiReportGroupMap>().Where(m => m.GroupId == groupId).ToListAsync(cancellationToken);
    }
}