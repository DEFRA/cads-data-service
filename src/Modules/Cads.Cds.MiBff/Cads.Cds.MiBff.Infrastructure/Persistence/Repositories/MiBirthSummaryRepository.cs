using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiBirthSummaryRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiBirthSummary, MiBffReadDbContext>(dbContext), IMiBirthSummaryRepository
{
    protected virtual IQueryable<MiBirthSummary> QueryBirthSummary(DateOnly fromDate, DateOnly toDate)
        => DbContext.GetBirthsSummary(fromDate, toDate);

    public async Task<IEnumerable<MiBirthSummary>> GetBirthSummaryAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default)
    {
        return await QueryBirthSummary(fromDate, toDate)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}