using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories;

public interface IMiReportRepository : IReadOnlyRepository<MiReport>
{
    Task<MiReport?> GetByReportIdAsync(Guid reportId, CancellationToken cancellationToken = default);

    Task<IEnumerable<MiBirthSummaryResult>> GetBirthSummaryAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default);
}