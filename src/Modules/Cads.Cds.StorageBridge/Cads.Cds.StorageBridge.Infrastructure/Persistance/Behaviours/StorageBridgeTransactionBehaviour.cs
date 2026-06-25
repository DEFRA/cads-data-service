using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Behaviours;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Infrastructure.Persistance.Behaviours;

[ExcludeFromCodeCoverage]
public class StorageBridgeTransactionBehaviour<TRequest, TResponse>(StorageBridgeWriteDbContext dbContext)
    : TransactionBehaviourBase<TRequest, TResponse, StorageBridgeWriteDbContext>(dbContext)
    where TRequest : ICommand<TResponse>
{
}