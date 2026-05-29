using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.StorageBridge.Core.Domain.Entities;
using Cads.Cds.StorageBridge.Core.Domain.Repositories;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.Repositories;

public class DataSeedIngestionHistoryRepository(StorageBridgeWriteDbContext dbContext)
    : EFRepository<DataSeedIngestionHistory, StorageBridgeWriteDbContext>(dbContext),
        IDataSeedIngestionHistoryRepository
{
    public async Task<DataSeedIngestionHistory?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await Query().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<DataSeedIngestionHistory?> GetByFileNameAsync(
        string fileName,
        CancellationToken cancellationToken = default)
    {
        return await Query().SingleOrDefaultAsync(x => x.FileName == fileName, cancellationToken);
    }
}