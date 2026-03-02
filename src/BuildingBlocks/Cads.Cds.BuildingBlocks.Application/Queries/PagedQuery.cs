using Cads.Cds.BuildingBlocks.Application.Queries;

namespace Cads.Cds.MiBff.Application.Queries
{
    public class PagedQuery<T> : IPagedQuery<T>
        where T : new()
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string? Order { get; set; }

        public string? Sort { get; set; }
    }
}