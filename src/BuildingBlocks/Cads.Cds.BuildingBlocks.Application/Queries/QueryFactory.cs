using Cads.Cds.BuildingBlocks.Application.Requests;

namespace Cads.Cds.BuildingBlocks.Application.Queries
{
    public static class QueryFactory
    {
        public static IPagedQuery<TDto> CreatePagedQuery<TQuery, TDto>(IPagedRequest request)
            where TQuery : IPagedQuery<TDto>,new()
        {
            return new TQuery
            {
                Page = request.Page ?? 1,
                PageSize = request.PageSize ?? 10,
                Order = request.Order,
                Sort = request.Sort
            };
        }
    }
}
