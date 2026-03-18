using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiReportGroupRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiReportGroup, MiBffReadDbContext>(dbContext), IMiReportGroupRepository
{
    public async Task<MiReportGroup?> GetByGroupIdAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        return await DbContext.ReportGroups.SingleOrDefaultAsync(g => g.GroupId == groupId, cancellationToken);
    }
}