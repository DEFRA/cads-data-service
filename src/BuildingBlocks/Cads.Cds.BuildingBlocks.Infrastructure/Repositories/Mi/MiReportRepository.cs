using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public class MiReportRepository(CadsReadOnlyDbContext dbContext)
    : ReadOnlyRepository<MiReport>(dbContext), IMiReportRepository
{
    public async Task<MiReport?> GetByReportIdAsync(Guid reportId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Reports.SingleOrDefaultAsync(r => r.ReportId == reportId, cancellationToken);
    }
}