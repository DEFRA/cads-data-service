using Asp.Versioning;
using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using Cads.Cds.Ingester.Application.Commands.AnimalMovements;
using Cads.Cds.Ingester.Core.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Cads.Cds.Ingester.Controllers.AnimalMovements.v2;

[ApiController]
[ApiVersion("2.0")]
[Authorize(Policy = AuthenticationConstants.DefaultPolicy)]
[Route("api/v{version:apiVersion}/nation/{nation}/animal-movements")]
public class AnimalMovementsV2Controller(IRequestExecutor executor, ILogger<AnimalMovementsV2Controller> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAnimalMovementsV2(
        Nation nation,
        [FromBody] EventMessage request)
    {
        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("Received animal movements request for {Nation} at {Timestamp}", nation, DateTime.UtcNow);
        }

        var payload = JsonSerializer.Serialize(request, JsonDefaults.DefaultOptionsWithIndented);
        var command = new AnimalMovementByNationCommand(nation, 2.0, payload);
        var response = await executor.ExecuteCommand(command);

        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("Received animal movements request for {Nation} at {Timestamp}", nation, DateTime.UtcNow);
        }

        return Accepted(response);
    }
}