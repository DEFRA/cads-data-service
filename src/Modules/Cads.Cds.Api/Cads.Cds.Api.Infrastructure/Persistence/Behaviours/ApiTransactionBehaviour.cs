using Cads.Cds.Api.Application.Commands;
using Cads.Cds.Api.Infrastructure.Persistence.Contexts;
using Cads.Cds.BuildingBlocks.Infrastructure.Persistence.Behaviours;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.Api.Infrastructure.Persistence.Behaviours;

[ExcludeFromCodeCoverage]
public class ApiTransactionBehaviour<TRequest, TResponse>(ApiWriteDbContext dbContext)
    : TransactionBehaviourBase<TRequest, TResponse, ApiWriteDbContext>(dbContext)
    where TRequest : IApiCommand<TResponse>
{
}