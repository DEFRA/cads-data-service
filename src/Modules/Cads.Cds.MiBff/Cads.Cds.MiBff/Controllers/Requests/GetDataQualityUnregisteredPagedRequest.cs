using Cads.Cds.BuildingBlocks.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers.Requests;

public class GetDataQualityUnregisteredPagedRequest : IPagedRequest
{
    [FromQuery] public int? Page { get; set; }

    [FromQuery] public int? PageSize { get; set; }

    [FromQuery] public string? Order { get; set; }

    [FromQuery] public string? Sort { get; set; }
}