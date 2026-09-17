using Cads.Cds.BuildingBlocks.Application.Queries.Sorting;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.Api.Controllers.Requests.Common;

public sealed class SortingOptionsRequest<TOrderBy> where TOrderBy : Enum
{
    [FromQuery(Name = "orderBy")]
    public TOrderBy OrderBy { get; init; } = default!;

    [FromQuery(Name = "direction")]
    public SortDirection Direction { get; init; } = SortDirection.Asc;
}