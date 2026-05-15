using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories;

public interface IMiImportSummaryRepository
{
    Task<IEnumerable<MiImportSummary>> GetImportSummaryAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default);
}