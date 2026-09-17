using Cads.Cds.Api.Application.Queries.Bovine.AnimalsOnHolding;
using Cads.Cds.Api.Controllers.Requests.Bovine;
using Cads.Cds.Api.Controllers.Requests.Common;
using Cads.Cds.BuildingBlocks.Application.Queries.Pagination;
using Cads.Cds.BuildingBlocks.Application.Queries.Sorting;

namespace Cads.Cds.Api.Controllers.Adapters.Bovine;

public static class AnimalsOnHoldingRequestAdapter
{
    public static GetAnimalsOnHolding ToQuery(
        GetAnimalsOnHoldingRequest request,
        AnimalsOnHoldingFilters filters,
        PagingOptionsRequest paging,
        SortingOptionsRequest<AnimalsOnHoldingOrderBy> sorting)
    {
        return new GetAnimalsOnHolding
        {
            Cph = request.Cph,
            HoldingAssociation = request.HoldingAssociation,

            Filters = new AnimalsOnHoldingFilters
            {
                Status = filters.Status,
                Sex = filters.Sex,
                BreedCode = filters.BreedCode,
                DateOnCphFrom = filters.DateOnCphFrom,
                Query = filters.Query
            },

            Paging = new PagingOptions
            {
                Page = paging.Page,
                PageSize = paging.PageSize
            },

            Sorting = new SortingOptions<AnimalsOnHoldingOrderBy>
            {
                OrderBy = sorting.OrderBy,
                Direction = sorting.Direction
            }
        };
    }
}