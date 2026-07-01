using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Behaviours;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;

namespace Cads.Cds.StorageBridge.Testing.Support.Fakes.Behaviours;

public class TestStorageBridgeCommitBehaviour<TRequest, TResponse>(StorageBridgeWriteDbContext dbContext)
    : TestCommitBehaviourBase<TRequest, TResponse>(dbContext)
    where TRequest : ICommand<TResponse>
{
}