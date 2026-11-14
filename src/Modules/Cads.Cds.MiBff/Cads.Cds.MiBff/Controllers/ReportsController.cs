using Asp.Versioning;
using Cads.Cds.BuildingBlocks.Application.Identity;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Application.Reports.Commands.CreateReportJob;
using Cads.Cds.MiBff.Application.Reports.Queries.GetReportJobFile;
using Cads.Cds.MiBff.Application.Reports.Queries.GetReportJobStatus;
using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;
using Cads.Cds.MiBff.Controllers.Extensions;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cads.Cds.MiBff.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Authorize(Policy = AuthenticationConstants.AadReportsReadPolicy)]
[Route("api/v{version:apiVersion}/bff/mi/reports")]
public class ReportsController(IMediator mediator, IReportRegistry reportRegistry, IUserContext userContext) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IReportRegistry _reportRegistry = reportRegistry;
    private readonly IUserContext _userContext = userContext;

    [HttpGet]
    [ProducesResponseType(typeof(ReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserReports(CancellationToken cancellationToken)
    {
        var query = new GetUserReportsQuery { Identifier = _userContext.UserIdentifier ?? Guid.NewGuid().ToString() };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{reportKey}/permissions")]
    [ProducesResponseType(typeof(UserReportPermissionsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserReportPermissions([FromRoute] string reportKey, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(reportKey))
            return BadRequest("Invalid reportKey");

        var query = new GetUserReportPermissionsQuery { ExternalSubject = _userContext.UserIdentifier ?? Guid.NewGuid().ToString(), ReportKey = reportKey };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess")]
    [HttpPost("{reportKey}")]
    [ProducesResponseType(typeof(OkObjectResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReport([FromRoute] string reportKey, CancellationToken cancellationToken)
    {
        var (Handler, RequestType) = _reportRegistry.Resolve(reportKey, HttpContext.RequestServices);

        var request = await DeserialiseReportRequest(RequestType, cancellationToken);

        if (request is null)
            return BadRequest("Invalid request payload");

        request.ReportKey = reportKey;

        var query = Handler.BuildUntypedQuery(request);

        var result = await _mediator.Send(query, cancellationToken);

        return result switch
        {
            FileReportResult file => this.XlsxFile(file),
            JsonReportResult json => Ok(json.Payload),
            _ => BadRequest("Unknown report result type")
        };
    }

    [Authorize(Policy = "ReportAccess")]
    [HttpPost("{reportKey}/jobs")]
    [ProducesResponseType(typeof(CreateReportJobResultDto), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RequestReport([FromRoute] string reportKey, CancellationToken cancellationToken)
    {
        // Validates that the reportKey is known/registered before accepting a job for it.
        var (_, requestType) = _reportRegistry.Resolve(reportKey, HttpContext.RequestServices);

        var request = await DeserialiseReportRequest(requestType, cancellationToken);

        if (request is null)
            return BadRequest("Invalid request payload");

        request.ReportKey = reportKey;

        var command = new CreateReportJobCommand
        {
            ReportKey = reportKey,
            ExternalSubject = _userContext.UserIdentifier ?? Guid.NewGuid().ToString(),
            Request = request
        };

        var reportJobId = await _mediator.Send(command, cancellationToken);

        return Accepted(new CreateReportJobResultDto { JobId = reportJobId });
    }

    [Authorize(Policy = "ReportAccess")]
    [HttpGet("{reportKey}/jobs/{jobId:guid}")]
    [ProducesResponseType(typeof(ReportJobStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReportJobStatus([FromRoute] string reportKey, [FromRoute] Guid jobId, CancellationToken cancellationToken)
    {
        var query = new GetReportJobStatusQuery { ReportJobId = jobId };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess")]
    [HttpGet("{reportKey}/job/{jobId:guid}")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OkObjectResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReportJobFile(
        [FromRoute] string reportKey,
        [FromRoute] Guid jobId,
        [FromQuery] string? fileType,
        CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<ReportFileType>(fileType ?? nameof(ReportFileType.Xlsx), ignoreCase: true, out var parsedFileType))
            return BadRequest("Invalid fileType. Expected 'json' or 'xlsx'.");

        var query = new GetReportJobFileQuery
        {
            ReportKey = reportKey,
            JobId = jobId,
            FileType = parsedFileType
        };

        var result = await _mediator.Send(query, cancellationToken);

        return result switch
        {
            FileReportResult file => this.XlsxFile(file),
            JsonReportResult json => Ok(json.Payload),
            _ => BadRequest("Unknown report result type")
        };
    }

    private async Task<GetReportRequest?> DeserialiseReportRequest(Type requestType, CancellationToken cancellationToken)
    {
        var request = (GetReportRequest?)await JsonSerializer.DeserializeAsync(
            HttpContext.Request.Body,
            requestType,
            JsonDefaults.DefaultOptionsWithStringEnumConversion,
            cancellationToken);

        return request;
    }
}