using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Behaviours;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Contexts;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Behaviours;

public class TestTransactionBehaviour<TRequest, TResponse>(FakeWriteDbContext dbContext)
: TransactionBehaviourBase<TRequest, TResponse, FakeWriteDbContext>(dbContext)
where TRequest : ICommand<TResponse>
{
}