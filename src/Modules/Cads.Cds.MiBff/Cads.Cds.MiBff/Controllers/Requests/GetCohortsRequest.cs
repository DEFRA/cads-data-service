using Cads.Cds.BuildingBlocks.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers.Requests;

public class GetCohortsRequest : IPagedRequest
{
    [FromRoute] public string AnimalId { get; set; } = string.Empty;

    [FromQuery] public int? Page { get; set; }

    [FromQuery] public int? PageSize { get; set; }

    [FromQuery] public string? Order { get; set; }

    [FromQuery] public string? Sort { get; set; }
}