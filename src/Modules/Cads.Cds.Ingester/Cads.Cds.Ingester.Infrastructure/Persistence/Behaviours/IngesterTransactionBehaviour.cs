using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Behaviours;
using Cads.Cds.Ingester.Infrastructure.Persistence.Contexts;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.Ingester.Infrastructure.Persistence.Behaviours;

[ExcludeFromCodeCoverage]
public class IngesterTransactionBehaviour<TRequest, TResponse>(IngesterWriteDbContext dbContext)
    : TransactionBehaviourBase<TRequest, TResponse, IngesterWriteDbContext>(dbContext)
    where TRequest : ICommand<TResponse>
{
}