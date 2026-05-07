using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Application.Identity;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Controllers.Requests.Reports;
using Cads.Cds.MiBff.Core.DTOs.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cads.Cds.MiBff.Controllers;

[Authorize(Policy = AuthenticationConstants.AadReportsReadPolicy)]
[ApiController]
[Route("api/v1/bff/mi/[controller]")]
public class ReportsController(IRequestExecutor executor, IUserContext userContext) : ControllerBase
{
    private readonly IRequestExecutor _executor = executor;
    private readonly IUserContext _userContext = userContext;

    [HttpGet]
    [ProducesResponseType(typeof(ReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserReports()
    {
        var query = new GetUserReportsQuery { Identifier = _userContext.UserIdentifier ?? Guid.NewGuid().ToString() };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [HttpGet("{reportKey}/permissions")]
    [ProducesResponseType(typeof(UserReportPermissionsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserReportPermissions([FromRoute] string reportKey)
    {
        var query = new GetUserReportPermissionsQuery { ExternalSubject = _userContext.UserIdentifier ?? Guid.NewGuid().ToString(), ReportKey = reportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:animal_summary")]
    [HttpPost("animal_summary")]
    [ProducesResponseType(typeof(PlaceholderReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAnimalSummaryReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:cohort_tracing")]
    [HttpPost("cohort_tracing")]
    [ProducesResponseType(typeof(PlaceholderReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCohortTracingReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:holding_summary")]
    [HttpPost("holding_summary")]
    [ProducesResponseType(typeof(PlaceholderReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHoldingSummaryReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:journey_by_haulier")]
    [HttpPost("journey_by_haulier")]
    [ProducesResponseType(typeof(PlaceholderReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetJourneyByHaulierReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:movements_all_holdings")]
    [HttpPost("movements_all_holdings")]
    [ProducesResponseType(typeof(PlaceholderReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMovementsAllHoldingsReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:movement_summary_holding")]
    [HttpPost("movement_summary_holding")]
    [ProducesResponseType(typeof(PlaceholderReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMovementSummaryHoldingReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:scrapie_flock_scheme_audit")]
    [HttpPost("scrapie_flock_scheme_audit")]
    [ProducesResponseType(typeof(PlaceholderReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetScrapieFlockSchemeAuditReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:sheep_goat_inspections")]
    [HttpPost("sheep_goat_inspections")]
    [ProducesResponseType(typeof(PlaceholderReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSheepGoatInspectionsReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:unregistered_herds_flocks")]
    [HttpPost("unregistered_herds_flocks")]
    [ProducesResponseType(typeof(PlaceholderReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUnregisteredHerdsFlocksReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:zonal_movements_summary")]
    [HttpPost("zonal_movements_summary")]
    [ProducesResponseType(typeof(PlaceholderReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetZonalMovementsSummaryReport(GetPlaceholderReportRequest request)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query);

        return Ok(result);
    }
}