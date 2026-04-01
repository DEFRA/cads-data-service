using System.Text.Json;
using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.Ingester.Application.Commands.AnimalMovements;
using Cads.Cds.Ingester.Controllers.Requests.AnimalMovements;
using Cads.Cds.Ingester.Core.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.Ingester.Controllers;

[ApiController]
[Authorize(Policy = AuthenticationConstants.ApiKeyOrCognitoPolicy)]
[Route("api/v1/regions/{nation}/animal-movements")]
public class AnimalMovementsController(IRequestExecutor executor, ILogger<AnimalMovementsController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAnimalMovements(
        Nation nation,
        [FromBody] AnimalMovementsRequest request)
    {
        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Received animal movements request for {nation} at {timestamp}", nation, DateTime.UtcNow);
        }

        var payload = JsonSerializer.Serialize<AnimalMovementsRequest>(request);
        var command = new AnimalMovementByNationCommand(nation, payload);
        var response = await executor.ExecuteCommand(command);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Animal movements request in storage at {IngestionId} and completed at {timestamp}", response.IngestionId, DateTime.UtcNow);
        }
        return Accepted(response);
    }
}