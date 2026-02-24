using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Application.Controllers.Requests;

public class GetHoldingsRequest
{
    [FromQuery] public int? Page { get; set; }
    [FromQuery] public int? PageSize { get; set; }
    [FromQuery] public string? Order { get; set; }
    [FromQuery] public string? Sort { get; set; }
    [FromQuery] public DateTime? LastModified { get; internal set; }
}
