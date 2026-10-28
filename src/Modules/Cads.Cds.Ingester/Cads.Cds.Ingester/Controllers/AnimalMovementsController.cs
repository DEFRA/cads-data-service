using Asp.Versioning;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.Ingester.Controllers.Requests.AnimalMovements;
using Cads.Cds.Ingester.Core.Domain.Enums;
using Cads.Cds.Ingester.Core.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.Ingester.Controllers.AnimalMovements;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Authorize(Policy = AuthenticationConstants.DefaultPolicy)]
[Route("api/v{version:apiVersion}/nation/{nation}/animal-movements")]
public class AnimalMovementsController(ILogger<AnimalMovementsController> logger) : ControllerBase
{
    [HttpPost]
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> PostAnimalMovementsV1(
        Nation nation,
        [FromBody] AnimalMovementsRequest request)
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

    [HttpPost]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> PostAnimalMovementsV2(
        Nation nation,
        [FromBody] AnimalMovementsRequest request)
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