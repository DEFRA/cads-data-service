using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.StorageBridge.Application.BulkLoad.Commands;
using Cads.Cds.StorageBridge.Controllers.Requests;
using Cads.Cds.StorageBridge.Controllers.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.StorageBridge.Controllers;

[ApiController]
[Authorize(Policy = AuthenticationConstants.ApiKeyOrCognitoPolicy)]
[Route("api/v1/storage/[controller]")]
public class S3BulkLoadController(IRequestExecutor executor) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    [HttpPost]
    public async Task<IActionResult> Execute([FromBody] S3BulkLoadRequest request, CancellationToken cancellationToken)
    {
        var command = new S3BulkLoadCommand
        {
            SourceKey = request.SourceKey,
            BulkImportType = request.BulkImportType,
            Delimiter = request.Delimiter,
            ActionType = request.ActionType
        };

        var jobId = await _executor.ExecuteCommand(command, cancellationToken);

        return Accepted(new JobResponse(jobId));
    }
}