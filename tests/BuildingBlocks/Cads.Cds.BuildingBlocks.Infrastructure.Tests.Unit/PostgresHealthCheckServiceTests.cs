
using Moq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit;

public class PostgresHealthCheckServiceTests
{
    [Fact]
    public async Task PostgresHealthCheckService_ReturnsHealthy()
    {
        var mockPostgresStatusService = new Mock<IPostgresStatusService>();
        mockPostgresStatusService.Setup(x => x.CanConnect(It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var unitUnderTest = new PostgresHealthCheckService(mockPostgresStatusService.Object);

        var healthCheckResult = await unitUnderTest.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);

        healthCheckResult.Status.Should().Be(HealthStatus.Healthy);
    }

    [Fact]
    public async Task PostgresHealthCheckService_ReturnsUnHealthy()
    {
        var mockPostgresStatusService = new Mock<IPostgresStatusService>();
        mockPostgresStatusService.Setup(x => x.CanConnect(It.IsAny<CancellationToken>())).ReturnsAsync(false);
        var unitUnderTest = new PostgresHealthCheckService(mockPostgresStatusService.Object);

        var healthCheckResult = await unitUnderTest.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);

        healthCheckResult.Status.Should().Be(HealthStatus.Unhealthy);
    }

    [Fact]
    public async Task PostgresStatusService_CanConnect_ReturnsTrue()
    {

        var dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("cads").Options);
        var postgresStatusService = new PostgresStatusService(dbContext);

        var canConnect = await postgresStatusService.CanConnect(CancellationToken.None);

        canConnect.Should().BeTrue();
    }
}