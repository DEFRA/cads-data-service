using System.Diagnostics.CodeAnalysis;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.MiBff.Core.Domain.Entities;
using Cads.Cds.MiBff.Core.Domain.Repositories;
using Cads.Cds.MiBff.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.MiBff.Infrastructure.Persistence.Repositories;

public class MiReportRepository(MiBffReadDbContext dbContext)
    : EFReadOnlyRepository<MiReport, MiBffReadDbContext>(dbContext), IMiReportRepository
{
    public async Task<MiReport?> GetByReportIdAsync(Guid reportId, CancellationToken cancellationToken = default)
    {
        return await Query().SingleOrDefaultAsync(r => r.ReportId == reportId, cancellationToken);
    }

    [ExcludeFromCodeCoverage]
    public async Task<IEnumerable<MiBirthSummaryResult>> GetBirthSummaryAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default)
    {
        return await DbContext.GetBirthsSummary(fromDate, toDate)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}