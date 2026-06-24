using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Cads.Cds.BuildingBlocks.Application.Commands;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Behaviours;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.Api.Infrastructure.Persistence.Behaviours;

[ExcludeFromCodeCoverage]
public class ApiTransactionBehaviour<TRequest, TResponse>(ApiWriteDbContext dbContext)
    : TransactionBehaviourBase<TRequest, TResponse, ApiWriteDbContext>(dbContext)
    where TRequest : ICommand<TResponse>
{
}