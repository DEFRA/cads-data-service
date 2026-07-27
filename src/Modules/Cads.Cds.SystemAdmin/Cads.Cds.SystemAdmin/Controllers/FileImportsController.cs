using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.CreateFileImport;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkCompleted;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkFailed;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkSplit;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkTransferred;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.ResetFileImport;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.UpdateFileImport;
using Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImportByFileName;
using Cads.Cds.SystemAdmin.Controllers.Requests.Imports;
using Cads.Cds.SystemAdmin.Core.DTOs.Imports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.SystemAdmin.Controllers;

[ApiController]
[Authorize(Policy = AuthenticationConstants.ApiKeyOrCognitoPolicy)]
[Route("api/v1/systemadmin/[controller]")]
public class FileImportsController(IRequestExecutor executor) : ControllerBase
{
    /// <summary>
    /// Used to check if a file has already been seen.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(FileImportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByFileName([FromQuery] string fileName, CancellationToken cancellationToken)
    {
        var query = new GetFileImportByFileNameQuery(fileName);

        var result = await executor.ExecuteQuery(query, cancellationToken);

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
            request.FileName,
            request.TotalRowsToProcess ?? 0,
            request.RowsFound ?? 0);

        var dto = await executor.ExecuteCommand(command, cancellationToken);

        return CreatedAtAction(nameof(GetByFileName), new { fileName = request.FileName }, dto);
    }

    /// <summary>
    /// Updates a new FileImport record
    /// Used to update the total row values.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateFileImportRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateFileImportCommand(
            id,
            request.TotalRowsToProcess ?? 0,
            request.RowsFound ?? 0,
            request.ImportStatus ?? FileImportStatus.Pending);

        await executor.ExecuteCommand(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Marks the import workflow as transferred.
    /// Used when the file has been transferred and decrypted into S3.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id:long}/transferred")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarkTransferred(long id, CancellationToken cancellationToken)
    {
        await executor.ExecuteCommand(new MarkFileTransferredCommand(id), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Marks the import workflow as split.
    /// Used when the file has been split into chunks in the internal S3 bucket.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id:long}/split")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarkSplit(long id, CancellationToken cancellationToken)
    {
        await executor.ExecuteCommand(new MarkFileSplitCommand(id), cancellationToken);

        return NoContent();
    }


    /// <summary>
    /// Marks the import workflow as complete.
    /// Used after a file (chunks) is successfully loaded into S3.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id:long}/completed")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarkCompleted(long id, CancellationToken cancellationToken)
    {
        await executor.ExecuteCommand(new MarkCompletedCommand(id), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Marks the import workflow as failed.
    /// Used if the S3 ingest (or splitting) fails.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id:long}/failed")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarkFailed(long id, CancellationToken cancellationToken)
    {
        await executor.ExecuteCommand(new MarkFailedCommand(id), cancellationToken);

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
        await executor.ExecuteCommand(new ResetFileImportCommand(id), cancellationToken);

        return NoContent();
    }
}