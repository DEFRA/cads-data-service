using Cads.Cds.MiBff.Application.Queries.Pagination;

namespace Cads.Cds.MiBff.Application.Queries;

public interface INonPagedQuery<T> : IQuery<NonPaginatedResult<T>>
{
}