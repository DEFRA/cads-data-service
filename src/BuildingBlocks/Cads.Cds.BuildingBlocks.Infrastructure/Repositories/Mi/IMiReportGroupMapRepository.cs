using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi
{
    public interface IMiReportGroupMapRepository : IReadOnlyRepository<MiReportGroupMap>
    {
        Task<IEnumerable<MiReportGroupMap>> GetByGroupIdAsync(Guid groupId, CancellationToken cancellationToken = default);
    }
}