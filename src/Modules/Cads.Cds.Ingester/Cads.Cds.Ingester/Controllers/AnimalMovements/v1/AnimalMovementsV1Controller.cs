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

namespace Cads.Cds.Ingester.Controllers.AnimalMovements.v1;

[ApiController]
[ApiVersion("1.0")]
[Authorize(Policy = AuthenticationConstants.DefaultPolicy)]
[Route("api/v{version:apiVersion}/nation/{nation}/animal-movements")]
public class AnimalMovementsV1Controller(IRequestExecutor executor, ILogger<AnimalMovementsV1Controller> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAnimalMovementsV1(
        Nation nation,
        [FromBody] AnimalMovementMessage request)
    {
        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("Received animal movements request for {Nation} at {Timestamp}", nation, DateTime.UtcNow);
        }

        var payload = JsonSerializer.Serialize(request, JsonDefaults.DefaultOptionsWithIndented);
        var command = new AnimalMovementByNationCommand(nation, 1.0, payload);
        var response = await executor.ExecuteCommand(command);

        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("Received animal movements request for {Nation} at {Timestamp}", nation, DateTime.UtcNow);
        }

        return Accepted(response);
    }
}