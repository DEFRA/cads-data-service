using Cads.Cds.BuildingBlocks.Application.Identity;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Application.Reports.Requests;
using Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;
using Cads.Cds.MiBff.Controllers.Extensions;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cads.Cds.MiBff.Controllers;

[Authorize(Policy = AuthenticationConstants.AadReportsReadPolicy)]
[ApiController]
[Route("api/v1/bff/mi/[controller]")]
public class ReportsController(IMediator mediator, IReportRegistry reportRegistry, IUserContext userContext) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IReportRegistry _reportRegistry = reportRegistry;
    private readonly IUserContext _userContext = userContext;

    [HttpGet]
    public async Task<IActionResult> GetUserReports(CancellationToken cancellationToken)
    {
        var query = new GetUserReportsQuery { Identifier = _userContext.UserIdentifier ?? Guid.NewGuid().ToString() };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{reportKey}/permissions")]
    public async Task<IActionResult> GetUserReportPermissions([FromRoute] string reportKey, CancellationToken cancellationToken)
    {
        var query = new GetUserReportPermissionsQuery { ExternalSubject = _userContext.UserIdentifier ?? Guid.NewGuid().ToString(), ReportKey = reportKey };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess")]
    [HttpPost("{reportKey}")]
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

    private async Task<GetReportRequest?> DeserialiseReportRequest(Type requestType, CancellationToken cancellationToken)
    {
        var request = (GetReportRequest?)await JsonSerializer.DeserializeAsync(
            HttpContext.Request.Body,
            requestType,
            JsonDefaults.DefaultOptions,
            cancellationToken);

        return request;
    }
}