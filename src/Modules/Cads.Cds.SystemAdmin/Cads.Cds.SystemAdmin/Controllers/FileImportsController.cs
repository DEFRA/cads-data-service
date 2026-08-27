using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.BatchUpdateFileImport;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.CreateFileImport;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.MarkFailed;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.ResetFileImport;
using Cads.Cds.SystemAdmin.Application.Imports.Commands.UpdateFileImport;
using Cads.Cds.SystemAdmin.Application.Imports.Mappings;
using Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImportByFileName;
using Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImportById;
using Cads.Cds.SystemAdmin.Application.Imports.Queries.GetFileImports;
using Cads.Cds.SystemAdmin.Controllers.Requests.Imports;
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
    /// Retrieves a FileImport record by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(FileImportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        var query = new GetFileImportByIdQuery(id);

        var result = await executor.ExecuteQuery(query, cancellationToken);

        return result is null ? NotFound() : Ok(result.FirstOrDefault());
    }

    /// <summary>
    /// retrieves a FileImport record by its ID, along with its group siblings if they exist.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:long}/group")]
    [ProducesResponseType(typeof(IEnumerable<FileImportDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetWithGroupSiblingsById([FromRoute] long id, CancellationToken cancellationToken)
    {
        var query = new GetFileImportByIdQuery(id, true);

        var result = await executor.ExecuteQuery(query, cancellationToken);

        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Retrieves a list of FileImport records based on the provided filters.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="groupKey"></param>
    /// <param name="fileImportStatus"></param>
    /// <param name="fileProcessingStatus"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FileImportDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? groupKey,
        [FromQuery] FileImportStatus? fileImportStatus = null,
        [FromQuery] FileProcessingStatus? fileProcessingStatus = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetFileImportsQuery(
            GroupKey: groupKey,
            FileImportStatus: fileImportStatus,
            FileProcessingStatus: fileProcessingStatus
        );

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
    [ProducesResponseType(typeof(FileImportDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateFileImportRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateFileImportCommand(
            request.FileName,
            request.TotalRowsToProcess ?? 0,
            request.RowsFound ?? 0);

        var fileImport = await executor.ExecuteCommand(command, cancellationToken);

        var dto = fileImport.MapToDto();

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
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateFileImportRequest request, CancellationToken cancellationToken)
    {
        if (request.ImportStatus == FileImportStatus.Failed)
        {
            return BadRequest("Use the /failed endpoint to mark a file import as failed.");
        }

        var command = new UpdateFileImportCommand(
            id,
            request.TotalRowsToProcess ?? 0,
            request.RowsFound ?? 0,
            request.RowsImported,
            request.LastFilePartImported,
            request.ImportStatus ?? FileImportStatus.Pending);

        await executor.ExecuteCommand(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Updates a group of FileImport records
    /// Used to update the total row values.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("batch")]
    [ProducesResponseType(typeof(object), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> BatchUpdate([FromBody] BatchUpdateFileImportRequest request, CancellationToken cancellationToken)
    {
        var command = new BatchUpdateFileImportCommand(
            request.GroupKey,
            request.TotalRowsToProcess,
            request.RowsFound,
            request.RowsImported,
            request.LastFilePartImported,
            request.ImportStatus ?? FileImportStatus.Pending);

        await executor.ExecuteCommand(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Marks the import workflow as failed with the reason provided.
    /// Used if the S3 ingest (or splitting) fails.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="reason"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id:long}/failed")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarkFailed(long id, [FromBody] string reason, CancellationToken cancellationToken)
    {
        await executor.ExecuteCommand(new MarkFailedCommand(id, reason), cancellationToken);
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
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Reset(long id, CancellationToken cancellationToken)
    {
        await executor.ExecuteCommand(new ResetFileImportCommand(id), cancellationToken);

        return NoContent();
    }
}