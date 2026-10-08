using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.Ingester.Controllers.Requests.AnimalMovements;
using Cads.Cds.Ingester.Core.Domain.Enums;
using Cads.Cds.Ingester.Core.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.Ingester.Controllers;

[ApiController]
[Authorize(Policy = AuthenticationConstants.ApiKeyOrCognitoPolicy)]
[Route("api/v1/nation/{nation}/animal-movements")]
public class AnimalMovementsController(ILogger<AnimalMovementsController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAnimalMovements(
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