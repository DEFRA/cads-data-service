using Cads.Cds.BuildingBlocks.Core.Persistence;
using Cads.Cds.Ingester.Core.Domain.Entities;

namespace Cads.Cds.Ingester.Core.Domain.Repositories;

public interface IDataSeedIngestionHistoryReadRepository : IReadOnlyRepository<DataSeedIngestionHistory>
{
    Task<DataSeedIngestionHistory?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<DataSeedIngestionHistory?> GetByFileNameAsync(string fileName, CancellationToken cancellationToken = default);
}