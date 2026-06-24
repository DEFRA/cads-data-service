using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Behaviours;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.Behaviours;

public class StorageBridgeTransactionBehaviour<TRequest, TResponse>(StorageBridgeWriteDbContext dbContext)
    : TransactionBehaviourBase<TRequest, TResponse, StorageBridgeWriteDbContext>(dbContext)
    where TRequest : ICommand<TResponse>
{
}