using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.StorageBridge.Application.Commands.BulkImport;
using Cads.Cds.StorageBridge.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.StorageBridge.Controllers;

[ApiController]
[Route("api/v1/storage/[controller]")]
public class ImportController(IRequestExecutor executor) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    [ExcludeFromCodeCoverage]
    [HttpPost]
    public async Task<IActionResult> Execute([FromBody] ImportRequest request)
    {
        var command = new BulkImportCommand
        {
            SourceKey = request.SourceKey,
            BulkImportType = request.BulkImportType,
            Delimiter = request.Delimiter
        };

        var result = await _executor.ExecuteCommand(command);

        return Ok(await Task.FromResult(result));
    }
}