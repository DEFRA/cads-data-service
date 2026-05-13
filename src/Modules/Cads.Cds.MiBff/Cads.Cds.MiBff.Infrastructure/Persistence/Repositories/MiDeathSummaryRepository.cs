using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiDeathSummaryRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiDeathSummary, MiBffReadDbContext>(dbContext), IMiDeathSummaryRepository    
{
    protected virtual IQueryable<MiDeathSummary> QueryDeathSummary(DateOnly fromDate, DateOnly toDate)
        => DbContext.GetDeathsSummary(fromDate, toDate);

    public async Task<IEnumerable<MiDeathSummary>> GetDeathSummaryAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default)
    {
        return await QueryDeathSummary(fromDate, toDate)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}