using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Transactions;
using Cads.Cds.StorageBridge.Application.Uow;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;

namespace Cads.Cds.StorageBridge.Testing.Support.Fakes.Uow;

public sealed class FakeStorageBridgeUnitOfWork(StorageBridgeWriteDbContext dbContext)
    : FakeManualUnitOfWork<StorageBridgeWriteDbContext>(dbContext), IStorageBridgeUnitOfWork;