using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.StorageBridge.Core.Domain.Entities;

namespace Cads.Cds.StorageBridge.Core.Domain.Repositories;

public interface IDataSeedIngestionHistoryReadRepository : IReadOnlyRepository<DataSeedIngestionHistory>
{
    Task<DataSeedIngestionHistory?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<DataSeedIngestionHistory?> GetByFileNameAsync(string fileName, CancellationToken cancellationToken = default);
}