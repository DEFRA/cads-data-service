using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public class MiReportGroupRepository(CadsReadOnlyDbContext dbContext)
    : ReadOnlyRepository<MiReportGroup>(dbContext), IMiReportGroupRepository
{
    public async Task<MiReportGroup?> GetByGroupIdAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        return await DbContext.ReportGroups.SingleOrDefaultAsync(g => g.GroupId == groupId, cancellationToken);
    }
}