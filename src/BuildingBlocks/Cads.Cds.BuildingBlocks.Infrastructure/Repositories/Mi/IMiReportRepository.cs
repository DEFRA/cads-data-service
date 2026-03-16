using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi;

public interface IMiReportRepository : IReadOnlyRepository<MiReport>
{
    Task<MiReport?> GetByReportIdAsync(Guid reportId, CancellationToken cancellationToken = default);
}