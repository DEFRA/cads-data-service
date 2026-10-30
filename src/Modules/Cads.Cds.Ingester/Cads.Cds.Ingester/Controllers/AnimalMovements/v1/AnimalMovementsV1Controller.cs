using Asp.Versioning;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.Ingester.Core.Domain.Enums;
using Cads.Cds.Ingester.Core.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.Ingester.Controllers.AnimalMovements.v1;

[ApiController]
[ApiVersion("1.0")]
[Authorize(Policy = AuthenticationConstants.DefaultPolicy)]
[Route("api/v{version:apiVersion}/nation/{nation}/animal-movements")]
public class AnimalMovementsV1Controller(ILogger<AnimalMovementsV1Controller> logger) : ControllerBase
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

        return Accepted(new IngestionDto
        {
            IngestionId = nation.ToString().ToLower(),
            RecordCount = 1
        });
    }
}