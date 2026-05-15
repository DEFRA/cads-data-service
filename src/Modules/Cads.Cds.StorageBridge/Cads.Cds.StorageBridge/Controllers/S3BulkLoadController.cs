using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.StorageBridge.Application.BulkLoad.Commands;
using Cads.Cds.StorageBridge.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Controllers;

[ApiController]
[Route("api/v1/storage/[controller]")]
public class S3BulkLoadController(IRequestExecutor executor) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    [ExcludeFromCodeCoverage]
    [HttpPost]
    public async Task<IActionResult> Execute([FromBody] S3BulkLoadRequest request)
    {
        var command = new S3BulkLoadCommand
        {
            SourceKey = request.SourceKey,
            BulkImportType = request.BulkImportType,
            Delimiter = request.Delimiter,
            ImportActionType = request.ActionType
        };

        var result = await _executor.ExecuteCommand(command);

        return Ok(result);
    }
}