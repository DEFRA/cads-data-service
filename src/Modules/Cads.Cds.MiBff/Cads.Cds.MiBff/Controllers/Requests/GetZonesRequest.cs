using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers.Requests;

public class GetZonesRequest
{
    [FromRoute] public string ZoneId { get; set; } = string.Empty;
}