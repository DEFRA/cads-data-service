using System.Text.Json;
using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.Ingester.Application.Commands.AnimalMovements;
using Cads.Cds.Ingester.Controllers.Requests.AnimalMovements;
using Cads.Cds.Ingester.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.Ingester.Controllers;

[ApiController]
[Route("api/v1/regions/{region}/animal-movements")]
public class AnimalMovementsController(IRequestExecutor executor) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAnimalMovements(
        Region region,
        [FromBody] AnimalMovementsRequest request)
    {
        var payload = JsonSerializer.Serialize<AnimalMovementsRequest>(request);
        var command = new AnimalMovementByRegionCommand(region, payload);
        var response = await executor.ExecuteCommand(command);
        return Accepted(response);
    }
}