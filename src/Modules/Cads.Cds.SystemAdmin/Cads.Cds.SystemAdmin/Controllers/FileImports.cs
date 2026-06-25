using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.SystemAdmin.Application.Imports.Commands;
using Cads.Cds.SystemAdmin.Application.Imports.Queries;
using Cads.Cds.SystemAdmin.Controllers.Requests.Imports;
using Cads.Cds.SystemAdmin.Core.DTOs.Imports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.SystemAdmin.Controllers;

[ApiController]
[Authorize(Policy = AuthenticationConstants.ApiKeyOrCognitoPolicy)]
[Route("api/v1/systemadmin/[controller]")]
public class FileImports(IRequestExecutor executor) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;

    /// <summary>
    /// Used to check if a file has already been seen.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("by-file-name/{fileName}")]
    [ProducesResponseType(typeof(FileImportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByFileName(string fileName, CancellationToken cancellationToken)
    {
        var query = new GetFileImportByFileNameQuery(fileName);

        var result = await _executor.ExecuteQuery(query, cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Creates a new FileImport record (Pending → Importing).
    /// Used when a new file is detected.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateFileImportRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateFileImportCommand(
            request.DestinationTableName,
            request.FileName,
            request.TotalRowsToProcess,
            request.RowsFound);

        var id = await _executor.ExecuteCommand(command, cancellationToken);

        return CreatedAtAction(nameof(GetByFileName), new { fileName = request.FileName }, new { id });
    }

    /// <summary>
    /// Marks the import workflow as complete.
    /// Used after a file (chunks) is successfully loaded into S3.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id:long}/complete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarkImportComplete(long id, CancellationToken cancellationToken)
    {
        await _executor.ExecuteCommand(new MarkFileImportCompleteCommand(id), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Marks the import workflow as failed.
    /// Used if the S3 ingest (or splitting) fails.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id:long}/fail")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarkImportFailed(long id, CancellationToken cancellationToken)
    {
        await _executor.ExecuteCommand(new MarkFileImportFailedCommand(id), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Resets the import to Pending for replay.
    /// Used when retrying a previously failed file.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id:long}/reset")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Reset(long id, CancellationToken cancellationToken)
    {
        await _executor.ExecuteCommand(new ResetFileImportCommand(id), cancellationToken);

        return NoContent();
    }
}
