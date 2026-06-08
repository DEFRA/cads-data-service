using Cads.Cds.MiBff.Application.Reports.Authorisation;
using Cads.Cds.MiBff.Controllers.Authorisation.Handlers;
using Cads.Cds.MiBff.Controllers.Authorisation.Requirements;
using Cads.Cds.MiBff.Testing.Support.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace Cads.Cds.MiBff.Tests.Component.Authorisation;

public class ReportAccessHandlerTests
{
    [Fact]
    public async Task HandleRequirementAsync_NoEmail_DoesNotSucceed()
    {
        // Arrange
        var requirement = new ReportAccessRequirement(TestReportKeyConstants.HoldingSummaryReportKey);

        var user = new ClaimsPrincipal(new ClaimsIdentity());
        var accessor = CreateHttpContextAccessor(user);

        var reportService = new Mock<IReportAccessService>(MockBehavior.Strict);
        reportService
            .Setup(s => s.HasReportAccessAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception("Should not be called"));

        var handler = new ReportAccessHandler(reportService.Object, accessor);

        var context = CreateAuthContext(requirement, user);

        // Act
        await handler.HandleAsync(context);

        // Assert
        Assert.False(context.HasSucceeded);
    }

    [Fact]
    public async Task HandleRequirementAsync_EmailPresent_AccessDenied_DoesNotSucceed()
    {
        // Arrange
        var requirement = new ReportAccessRequirement(TestReportKeyConstants.HoldingSummaryReportKey);

        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim(ClaimTypes.Email, "test@internal.test")],
            "TestAuth"
        ));

        var accessor = CreateHttpContextAccessor(user);

        var reportService = new Mock<IReportAccessService>();
        reportService
            .Setup(s => s.HasReportAccessAsync("test@internal.test", TestReportKeyConstants.HoldingSummaryReportKey, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var handler = new ReportAccessHandler(reportService.Object, accessor);

        var context = CreateAuthContext(requirement, user);

        // Act
        await handler.HandleAsync(context);

        // Assert
        Assert.False(context.HasSucceeded);
    }

    [Fact]
    public async Task HandleRequirementAsync_EmailPresent_AccessGranted_Succeeds()
    {
        // Arrange
        var requirement = new ReportAccessRequirement(TestReportKeyConstants.HoldingSummaryReportKey);

        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim(ClaimTypes.Email, "test@internal.test")],
            "TestAuth"
        ));

        var accessor = CreateHttpContextAccessor(user);

        var reportService = new Mock<IReportAccessService>();
        reportService
            .Setup(s => s.HasReportAccessAsync("test@internal.test", TestReportKeyConstants.HoldingSummaryReportKey, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new ReportAccessHandler(reportService.Object, accessor);

        var context = CreateAuthContext(requirement, user);

        // Act
        await handler.HandleAsync(context);

        // Assert
        Assert.True(context.HasSucceeded);
    }

    private static AuthorizationHandlerContext CreateAuthContext(
        ReportAccessRequirement requirement,
        ClaimsPrincipal user)
    {
        return new AuthorizationHandlerContext(
            requirements: [requirement],
            user: user,
            resource: null
        );
    }

    private static IHttpContextAccessor CreateHttpContextAccessor(ClaimsPrincipal user)
    {
        var httpContext = new DefaultHttpContext
        {
            User = user
        };

        var accessor = new Mock<IHttpContextAccessor>();
        accessor.Setup(a => a.HttpContext).Returns(httpContext);

        return accessor.Object;
    }
}