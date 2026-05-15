using Cads.Cds.MiBff.Controllers.Authorisation.Handlers;
using Cads.Cds.MiBff.Controllers.Authorisation.Requirements;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Cads.Cds.MiBff.Tests.Component.Authorisation;

public class DynamicReportAccessHandlerTests
{
    private readonly DynamicReportAccessHandler _handler;
    private readonly FakeAuthorizationService _authService;
    private readonly IServiceProvider _provider;

    public DynamicReportAccessHandlerTests()
    {
        var services = new ServiceCollection();

        _authService = new FakeAuthorizationService();
        services.AddSingleton<IAuthorizationService>(_authService);

        _provider = services.BuildServiceProvider();

        _handler = new DynamicReportAccessHandler(_provider);
    }

    private static AuthorizationHandlerContext CreateContext(object resource)
    {
        var requirement = new DynamicReportAccessRequirement();
        return new AuthorizationHandlerContext(
            [requirement],
            user: new ClaimsPrincipal(),
            resource: resource);
    }

    [Fact]
    public async Task HandleRequirementAsync_ShouldDoNothing_WhenResourceIsInvalid()
    {
        var context = CreateContext(resource: new object());

        await _handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    [Fact]
    public async Task HandleRequirementAsync_ShouldDoNothing_WhenReportKeyMissing()
    {
        var http = new DefaultHttpContext();
        var context = CreateContext(http);

        await _handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    [Fact]
    public async Task HandleRequirementAsync_ShouldDoNothing_WhenReportKeyEmpty()
    {
        var http = new DefaultHttpContext();
        http.Request.RouteValues["reportKey"] = "";

        var context = CreateContext(http);

        await _handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    [Fact]
    public async Task HandleRequirementAsync_ShouldNotSucceed_WhenAuthorizationFails()
    {
        var http = new DefaultHttpContext();
        http.Request.RouteValues["reportKey"] = "holding_summary";

        _authService.ShouldSucceed = false;

        var context = CreateContext(http);

        await _handler.HandleAsync(context);

        context.HasSucceeded.Should().BeFalse();
    }

    [Fact]
    public async Task HandleRequirementAsync_ShouldSucceed_WhenAuthorizationSucceeds()
    {
        var http = new DefaultHttpContext();
        http.Request.RouteValues["reportKey"] = "holding_summary";

        _authService.ShouldSucceed = true;

        var context = CreateContext(http);

        await _handler.HandleAsync(context);

        context.HasSucceeded.Should().BeTrue();
    }

    [Fact]
    public async Task HandleRequirementAsync_ShouldWork_WhenResourceIsAuthorizationFilterContext()
    {
        var http = new DefaultHttpContext();
        http.Request.RouteValues["reportKey"] = "gb_cattle_registrations";

        var actionContext = new ActionContext(
            http,
            new RouteData(),
            new ActionDescriptor()
        );

        var filterContext = new AuthorizationFilterContext(
            actionContext,
            []
        );

        _authService.ShouldSucceed = true;

        var context = CreateContext(filterContext);

        await _handler.HandleAsync(context);

        context.HasSucceeded.Should().BeTrue();
    }

    private class FakeAuthorizationService : IAuthorizationService
    {
        public bool ShouldSucceed { get; set; }

        public Task<AuthorizationResult> AuthorizeAsync(
            ClaimsPrincipal user,
            object? resource,
            IEnumerable<IAuthorizationRequirement> requirements)
        {
            return Task.FromResult(
                ShouldSucceed ? AuthorizationResult.Success() : AuthorizationResult.Failed());
        }

        public Task<AuthorizationResult> AuthorizeAsync(
            ClaimsPrincipal user,
            object? resource,
            string policyName)
        {
            return Task.FromResult(
                ShouldSucceed ? AuthorizationResult.Success() : AuthorizationResult.Failed());
        }
    }
}