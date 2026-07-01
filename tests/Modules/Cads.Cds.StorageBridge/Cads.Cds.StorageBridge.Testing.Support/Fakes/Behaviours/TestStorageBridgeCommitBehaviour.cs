using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Behaviours;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;

namespace Cads.Cds.StorageBridge.Testing.Support.Fakes.Behaviours;

public class TestStorageBridgeCommitBehaviour<TRequest, TResponse>(SystemAdminWriteDbContext dbContext)
    : TestCommitBehaviourBase<TRequest, TResponse>(dbContext)
    where TRequest : ICommand<TResponse>
{
}
