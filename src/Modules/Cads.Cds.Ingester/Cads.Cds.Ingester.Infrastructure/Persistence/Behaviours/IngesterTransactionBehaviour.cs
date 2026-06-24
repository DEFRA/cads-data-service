using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Behaviours;
using Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;

namespace Cads.Cds.Ingester.Infrastructure.Persistence.Behaviours;

public class IngesterTransactionBehaviour<TRequest, TResponse>(IngesterWriteDbContext dbContext)
    : TransactionBehaviourBase<TRequest, TResponse, IngesterWriteDbContext>(dbContext)
    where TRequest : ICommand<TResponse>
{
}