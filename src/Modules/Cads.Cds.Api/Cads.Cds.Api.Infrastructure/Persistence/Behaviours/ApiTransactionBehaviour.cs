using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Behaviours;

namespace Cads.Cds.Api.Infrastructure.Persistence.Behaviours;

public class ApiTransactionBehaviour<TRequest, TResponse>(ApiWriteDbContext dbContext)
    : TransactionBehaviourBase<TRequest, TResponse, ApiWriteDbContext>(dbContext)
    where TRequest : ICommand<TResponse>
{
}