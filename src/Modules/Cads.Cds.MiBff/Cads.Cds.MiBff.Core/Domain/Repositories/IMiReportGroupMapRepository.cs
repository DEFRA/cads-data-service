using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.MiBff.Core.Domain.Entities;

namespace Cads.Cds.MiBff.Core.Domain.Repositories
{
    public interface IMiReportGroupMapRepository : IReadOnlyRepository<MiReportGroupMap>
    {
        Task<IReadOnlyList<MiReportGroupMap>> GetByGroupIdAsync(Guid groupId, CancellationToken cancellationToken = default);
    }
}