using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers.Requests.Ukv;

public class GetHoldingsRequest
{
    [FromRoute] public required string Cph { get; set; }
}