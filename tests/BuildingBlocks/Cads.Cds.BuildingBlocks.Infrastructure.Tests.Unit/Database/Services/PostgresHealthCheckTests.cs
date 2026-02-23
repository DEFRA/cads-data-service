using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Health;
using Cads.Cds.BuildingBlocks.Infrastructure.Database.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Database.Services;

public class PostgresHealthCheckTests
{
    [Fact]
    public async Task PostgresHealthCheckService_ReturnsHealthy()
    {
        var mockPostgresStatusService = new Mock<IPostgresStatusService>();
        mockPostgresStatusService.Setup(x => x.CanConnect(It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var unitUnderTest = new PostgresHealthCheck(mockPostgresStatusService.Object);

        var healthCheckResult = await unitUnderTest.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);

        healthCheckResult.Status.Should().Be(HealthStatus.Healthy);
    }

    [Fact]
    public async Task PostgresHealthCheckService_ReturnsUnHealthy()
    {
        var mockPostgresStatusService = new Mock<IPostgresStatusService>();
        mockPostgresStatusService.Setup(x => x.CanConnect(It.IsAny<CancellationToken>())).ReturnsAsync(false);
        var unitUnderTest = new PostgresHealthCheck(mockPostgresStatusService.Object);

        var healthCheckResult = await unitUnderTest.CheckHealthAsync(new HealthCheckContext(), CancellationToken.None);

        healthCheckResult.Status.Should().Be(HealthStatus.Unhealthy);
    }

    [Fact]
    public async Task PostgresStatusService_CanConnect_ReturnsTrue()
    {
        var dbContext = new HealthCheckDbContext(new DbContextOptionsBuilder<HealthCheckDbContext>().UseInMemoryDatabase("cads").Options);
        var postgresStatusService = new PostgresStatusService(new List<HealthCheckDbContext> { dbContext });

        var canConnect = await postgresStatusService.CanConnect(CancellationToken.None);

        canConnect.Should().BeTrue();
    }
}