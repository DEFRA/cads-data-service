using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.Api.Controllers.Requests.Common;

public sealed class PagingOptionsRequest
{
    [FromQuery(Name = "page")]
    public int Page { get; init; } = 1;

    [FromQuery(Name = "pageSize")]
    public int PageSize { get; init; } = 25;
}