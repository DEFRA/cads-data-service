using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.StorageBridge.Application.S3Import.Commands;
using Cads.Cds.StorageBridge.Controllers.Requests;
using Cads.Cds.StorageBridge.Controllers.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.StorageBridge.Controllers;

[ApiController]
[Authorize(Policy = AuthenticationConstants.ApiKeyOrCognitoPolicy)]
[Route("api/v1/storage/[controller]")]
public class S3ImportController(IRequestExecutor executor) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    [HttpPost("csv-import")]
    public async Task<IActionResult> ExecuteCsvImport([FromBody] S3CsvImportRequest request, CancellationToken cancellationToken)
    {
        var command = new S3CsvImportCommand
        {
            SourceKey = request.SourceKey,
            ImportDataType = request.ImportDataType,
            Delimiter = request.Delimiter,
            ImportActionType = request.ImportActionType
        };

        var jobId = await _executor.ExecuteCommand(command, cancellationToken);

        return Accepted(new JobResponse(jobId));
    }

    [HttpPost("sql-import")]
    public async Task<IActionResult> ExecuteSqlImport([FromBody] S3SqlImportRequest request, CancellationToken cancellationToken)
    {
        var command = new S3SqlImportCommand
        {
            SourceKey = request.SourceKey
        };

        var jobId = await _executor.ExecuteCommand(command, cancellationToken);

        return Accepted(new JobResponse(jobId));
    }
}