using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Repositories;
using Cads.Cds.StorageBridge.Core.Domain.Entities;
using Cads.Cds.StorageBridge.Core.Domain.Repositories;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.Repositories;

public class DataSeedIngestionHistoryWriteRepository(StorageBridgeWriteDbContext dbContext)
    : EFRepository<DataSeedIngestionHistory, StorageBridgeWriteDbContext>(dbContext),
        IDataSeedIngestionHistoryWriteRepository
{
}