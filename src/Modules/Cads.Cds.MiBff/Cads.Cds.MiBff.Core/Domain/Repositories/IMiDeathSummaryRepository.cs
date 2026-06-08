using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories;

public interface IMiDeathSummaryRepository
{
    Task<IEnumerable<MiDeathSummary>> GetDeathSummaryAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default);
}