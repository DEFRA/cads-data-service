using Cads.Cds.BuildingBlocks.Application.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Behaviours;

public abstract class TestCommitBehaviourBase<TRequest, TResponse>(DbContext dbContext)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly DbContext _dbContext = dbContext;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next(cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return response;
    }
}