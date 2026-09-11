using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.SystemAdmin.Application.Generation.Commands.CreateGeneration;
using Cads.Cds.SystemAdmin.Controllers.Requests.Generation;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.SystemAdmin.Controllers;

[ApiController]
[Authorize(Policy = AuthenticationConstants.ApiKeyOrCognitoPolicy)]
[Route("api/v1/systemadmin/[controller]")]
public class GenerationController(IRequestExecutor executor) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateGenerationResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateGenerationRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateGenerationCommand(
            request.Table,
            request.Scenario,
            request.RowCount ?? 0,
            request.BusinessKey);

        var response = await executor.ExecuteCommand(command, cancellationToken);

        return CreatedAtAction(nameof(Create), new { fileName = request.Table, scenario = request.Scenario, rowCount = request.RowCount, businessKey = request.BusinessKey }, response);
    }
}