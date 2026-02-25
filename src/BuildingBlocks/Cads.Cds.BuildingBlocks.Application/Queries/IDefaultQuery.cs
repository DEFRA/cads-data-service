using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;

namespace Cads.Cds.BuildingBlocks.Application.Queries;

public interface IDefaultQuery<T> : IQuery<DefaultResult<T>>
{
}