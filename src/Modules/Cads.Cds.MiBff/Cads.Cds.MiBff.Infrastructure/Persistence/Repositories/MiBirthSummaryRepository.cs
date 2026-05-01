using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiBirthSummaryRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiBirthSummary, MiBffReadDbContext>(dbContext), IMiBirthSummaryRepository
{
    public async Task<IEnumerable<MiBirthSummary>> GetBirthSummaryAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default)
    {
        return await DbContext.GetBirthsSummary(fromDate, toDate)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
