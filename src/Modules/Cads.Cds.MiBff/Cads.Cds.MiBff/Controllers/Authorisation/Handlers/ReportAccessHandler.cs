using Cads.Cds.BuildingBlocks.Infrastructure.Identity;
using Cads.Cds.MiBff.Controllers.Authorisation.Requirements;
using Cads.Cds.MiBff.Core.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Cads.Cds.MiBff.Controllers.Authorisation.Handlers;

public class ReportAccessHandler(IReportAccessService reportService,
    IHttpContextAccessor httpContextAccessor)
    : AuthorizationHandler<ReportAccessRequirement>
{
    private readonly IReportAccessService _reportService = reportService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ReportAccessRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var user = httpContext?.User;
        var email = user?.GetEmail();

        if (email is null)
            return;

        var ct = _httpContextAccessor.HttpContext?.RequestAborted
                 ?? CancellationToken.None;

        var granted = await _reportService.HasReportAccessAsync(
            email,
            requirement.ReportKey,
            ct);

        if (granted)
            context.Succeed(requirement);
    }
}