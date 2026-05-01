using Cads.Cds.MiBff.Controllers.Authorisation.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cads.Cds.MiBff.Controllers.Authorisation.Handlers;

public class DynamicReportAccessHandler(IAuthorizationService authorizationService) : AuthorizationHandler<DynamicReportAccessRequirement>
{
    private readonly IAuthorizationService _authorizationService = authorizationService;

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DynamicReportAccessRequirement requirement)
    {
        var httpContext = context.Resource switch
        {
            HttpContext ctx => ctx,
            AuthorizationFilterContext filter => filter.HttpContext,
            _ => null
        };

        if (httpContext is null)
            return;

        if (!httpContext.Request.RouteValues.TryGetValue("reportKey", out var keyObj))
            return;

        var reportKey = keyObj?.ToString();
        if (string.IsNullOrWhiteSpace(reportKey))
            return;

        // Re-run authorization using the static policy
        var result = await _authorizationService.AuthorizeAsync(
            context.User,
            resource: null,
            policyName: $"ReportAccess:{reportKey}");

        if (result.Succeeded)
            context.Succeed(requirement);
    }
}
