using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Behaviours;
using Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;

namespace Cads.Cds.Ingester.Testing.Support.Fakes.Behaviours;

public class TestIngesterCommitBehaviour<TRequest, TResponse>(IngesterWriteDbContext dbContext)
    : TestCommitBehaviourBase<TRequest, TResponse>(dbContext)
    where TRequest : ICommand<TResponse>
{
}