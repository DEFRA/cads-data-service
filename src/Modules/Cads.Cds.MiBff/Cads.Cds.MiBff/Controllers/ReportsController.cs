using Cads.Cds.BuildingBlocks.Application;
using Cads.Cds.BuildingBlocks.Application.Identity;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.MiBff.Application.Queries.Reports;
using Cads.Cds.MiBff.Controllers.Extensions;
using Cads.Cds.MiBff.Controllers.Requests.Reports;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> GetUserReports(CancellationToken cancellationToken)
    {
        var query = new GetUserReportsQuery { Identifier = _userContext.UserIdentifier ?? Guid.NewGuid().ToString() };

        var result = await _executor.ExecuteQuery(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{reportKey}/permissions")]
    public async Task<IActionResult> GetUserReportPermissions([FromRoute] string reportKey, CancellationToken cancellationToken)
    {
        var query = new GetUserReportPermissionsQuery { ExternalSubject = _userContext.UserIdentifier ?? Guid.NewGuid().ToString(), ReportKey = reportKey };

        var result = await _executor.ExecuteQuery(query, cancellationToken);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:gb_cattle_registrations")]
    [HttpPost("gb_cattle_registrations")]
    public async Task<IActionResult> GetGbCattleRegistrationsReport(GetGbCattleRegistrationsReportRequest request, CancellationToken cancellationToken)
    {
        var result = await _executor.ExecuteQuery(new GetGbCattleRegistrationsQuery(request.ReportKey, request.Year, request.Month), cancellationToken);

        return this.XlsxFile(result);
    }

    [Authorize(Policy = "ReportAccess:animal_summary")]
    [HttpPost("animal_summary")]
    public async Task<IActionResult> GetAnimalSummaryReport(GetPlaceholderReportRequest request, CancellationToken cancellationToken)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query, cancellationToken);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:cohort_tracing")]
    [HttpPost("cohort_tracing")]
    public async Task<IActionResult> GetCohortTracingReport(GetPlaceholderReportRequest request, CancellationToken cancellationToken)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query, cancellationToken);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:holding_summary")]
    [HttpPost("holding_summary")]
    public async Task<IActionResult> GetHoldingSummaryReport(GetPlaceholderReportRequest request, CancellationToken cancellationToken)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query, cancellationToken);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:journey_by_haulier")]
    [HttpPost("journey_by_haulier")]
    public async Task<IActionResult> GetJourneyByHaulierReport(GetPlaceholderReportRequest request, CancellationToken cancellationToken)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query, cancellationToken);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:movements_all_holdings")]
    [HttpPost("movements_all_holdings")]
    public async Task<IActionResult> GetMovementsAllHoldingsReport(GetPlaceholderReportRequest request, CancellationToken cancellationToken)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query, cancellationToken);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:movement_summary_holding")]
    [HttpPost("movement_summary_holding")]
    public async Task<IActionResult> GetMovementSummaryHoldingReport(GetPlaceholderReportRequest request, CancellationToken cancellationToken)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query, cancellationToken);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:scrapie_flock_scheme_audit")]
    [HttpPost("scrapie_flock_scheme_audit")]
    public async Task<IActionResult> GetScrapieFlockSchemeAuditReport(GetPlaceholderReportRequest request, CancellationToken cancellationToken)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query, cancellationToken);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:sheep_goat_inspections")]
    [HttpPost("sheep_goat_inspections")]
    public async Task<IActionResult> GetSheepGoatInspectionsReport(GetPlaceholderReportRequest request, CancellationToken cancellationToken)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query, cancellationToken);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:unregistered_herds_flocks")]
    [HttpPost("unregistered_herds_flocks")]
    public async Task<IActionResult> GetUnregisteredHerdsFlocksReport(GetPlaceholderReportRequest request, CancellationToken cancellationToken)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query, cancellationToken);

        return Ok(result);
    }

    [Authorize(Policy = "ReportAccess:zonal_movements_summary")]
    [HttpPost("zonal_movements_summary")]
    public async Task<IActionResult> GetZonalMovementsSummaryReport(GetPlaceholderReportRequest request, CancellationToken cancellationToken)
    {
        var query = new GetPlaceholderReportQuery { ReportKey = request.ReportKey };

        var result = await _executor.ExecuteQuery(query, cancellationToken);

        return Ok(result);
    }
}