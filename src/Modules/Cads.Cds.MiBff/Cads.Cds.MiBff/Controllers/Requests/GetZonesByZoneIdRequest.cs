using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers.Requests;

public class GetZonesByZoneIdRequest
{
    [FromRoute] public required Guid ZoneId { get; set; }
}