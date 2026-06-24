using Cads.Cds.BuildingBlocks.Application.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Database.Behaviours;

public abstract class TransactionBehaviourBase<TRequest, TResponse, TDbContext>(TDbContext dbContext)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TDbContext : CadsDbContext
{
    private readonly TDbContext _dbContext = dbContext;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not ITransactionalCommand)
            return await next(cancellationToken);

        var strategy = _dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var tx = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var response = await next();

                await _dbContext.SaveChangesAsync(cancellationToken);
                await tx.CommitAsync(cancellationToken);

                return response;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }
}