using Cads.Cds.BuildingBlocks.Core.Domain.Entities.Mi;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Repositories.Mi
{
    public interface IMiReportGroupRepository : IReadOnlyRepository<MiReportGroup>
    {
        Task<MiReportGroup?> GetByGroupIdAsync(Guid groupId, CancellationToken cancellationToken = default);
    }
}