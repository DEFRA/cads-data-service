using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiImportSummaryRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiImportSummary, MiBffReadDbContext>(dbContext), IMiImportSummaryRepository
{
    protected virtual IQueryable<MiImportSummary> QueryImportSummary(DateOnly fromDate, DateOnly toDate)
        => DbContext.GetImportsSummary(fromDate, toDate);

    public async Task<IEnumerable<MiImportSummary>> GetImportSummaryAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default)
    {
        return await QueryImportSummary(fromDate, toDate)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}