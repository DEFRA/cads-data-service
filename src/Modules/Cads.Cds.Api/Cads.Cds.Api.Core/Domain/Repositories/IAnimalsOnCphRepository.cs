using Cads.Cds.Api.Core.Domain.Paging;
using Cads.Cds.Api.Core.DTOs.Bovine;

namespace Cads.Cds.Api.Core.Domain.Repositories;

public interface IAnimalsOnCphRepository
{
    Task<bool> HoldingExistsAsync(string cph, CancellationToken cancellationToken = default);

    Task<PagedResult<TResult>> QueryAnimalsAsync<TResult>(
        string cph,
        Func<IQueryable<AnimalOnCphDto>, IQueryable<TResult>> shape,
        Func<IQueryable<TResult>, IOrderedQueryable<TResult>> order,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}