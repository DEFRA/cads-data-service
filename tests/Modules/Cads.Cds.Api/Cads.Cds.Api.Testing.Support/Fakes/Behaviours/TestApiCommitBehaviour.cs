using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Behaviours;

namespace Cads.Cds.Api.Testing.Support.Fakes.Behaviours;

public class TestApiCommitBehaviour<TRequest, TResponse>(ApiWriteDbContext dbContext)
    : TestCommitBehaviourBase<TRequest, TResponse>(dbContext)
    where TRequest : ICommand<TResponse>
{
}