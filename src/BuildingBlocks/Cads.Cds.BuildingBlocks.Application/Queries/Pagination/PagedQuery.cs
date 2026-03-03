namespace Cads.Cds.BuildingBlocks.Application.Queries.Pagination
{
    public class PagedQuery<T> : IPagedQuery<T>
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? Order { get; set; }

        public string? Sort { get; set; }
    }
}