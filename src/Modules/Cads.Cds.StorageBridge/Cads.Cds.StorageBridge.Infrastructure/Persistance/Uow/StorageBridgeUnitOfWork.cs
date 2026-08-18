using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Uow;
using Cads.Cds.StorageBridge.Application.Uow;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.Uow;

internal sealed class StorageBridgeUnitOfWork(StorageBridgeWriteDbContext dbContext)
    : ManualUnitOfWork<StorageBridgeWriteDbContext>(dbContext), IStorageBridgeUnitOfWork;