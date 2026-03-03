using Cads.Cds.BuildingBlocks.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers.Requests;

public class GetHoldingsRequest
{
    [FromRoute] public required string Cph { get; set; }
}