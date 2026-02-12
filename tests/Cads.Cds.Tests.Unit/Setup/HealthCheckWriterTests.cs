using Cads.Cds.Setup;
using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cads.Cds.Tests.Unit.Setup;

public class HealthCheckWriterTests
{
    [Fact]
    public void WriteHealthStatusAsJson_WritesStatusAndDuration()
    {
        var report = new HealthReport(
            new Dictionary<string, HealthReportEntry>(),
            TimeSpan.FromMilliseconds(123));

        var json = HealthCheckWriter.WriteHealthStatusAsJson(report, false, false, false);

        json.Should().Contain("\"status\":\"Healthy\"");
        json.Should().Contain("\"durationMs\":123");
    }

    [Fact]
    public void WriteHealthStatusAsJson_WhenMaskingEnabled_DoesNotIncludeResults()
    {
        var entry = new HealthReportEntry(
            HealthStatus.Unhealthy,
            "desc",
            TimeSpan.FromMilliseconds(10),
            exception: null,
            data: null,
            tags: null);

        var report = new HealthReport(
            new Dictionary<string, HealthReportEntry> { ["db"] = entry },
            TimeSpan.Zero);

        var json = HealthCheckWriter.WriteHealthStatusAsJson(report, healthcheckMaskingEnabled: true, excludeHealthy: false, indented: false);

        json.Should().NotContain("results");
    }

    [Fact]
    public void WriteHealthStatusAsJson_WhenExcludeHealthy_OnlyWritesUnhealthyEntries()
    {
        var healthy = new HealthReportEntry(HealthStatus.Healthy, "ok", TimeSpan.FromMilliseconds(1), exception: null, data: null, tags: null);
        var unhealthy = new HealthReportEntry(HealthStatus.Unhealthy, "bad", TimeSpan.FromMilliseconds(2), exception: null, data: null, tags: null);

        var report = new HealthReport(
            new Dictionary<string, HealthReportEntry>
            {
                ["healthy"] = healthy,
                ["unhealthy"] = unhealthy
            },
            TimeSpan.Zero);

        var json = HealthCheckWriter.WriteHealthStatusAsJson(report, false, excludeHealthy: true, indented: false);

        json.Should().Contain("\"unhealthy\"");
        json.Should().NotContain("\"healthy\"");
    }

    [Fact]
    public void WriteHealthStatusAsJson_WritesEntryDetails()
    {
        var entry = new HealthReportEntry(
            HealthStatus.Degraded,
            "slow",
            TimeSpan.FromMilliseconds(50),
            exception: null,
            data: null,
            tags: null);

        var report = new HealthReport(
            new Dictionary<string, HealthReportEntry> { ["cache"] = entry },
            TimeSpan.Zero);

        var json = HealthCheckWriter.WriteHealthStatusAsJson(report, false, false, false);

        json.Should().Contain("\"cache\"");
        json.Should().Contain("\"status\":\"Degraded\"");
        json.Should().Contain("\"description\":\"slow\"");
        json.Should().Contain("\"durationMs\":50");
    }

    [Fact]
    public void WriteHealthStatusAsJson_WritesTags()
    {
        var entry = new HealthReportEntry(
            HealthStatus.Healthy,
            "ok",
            TimeSpan.Zero,
            exception: null,
            data: null,
            tags: ["db", "critical"]);

        var report = new HealthReport(
            new Dictionary<string, HealthReportEntry> { ["db"] = entry },
            TimeSpan.Zero);

        var json = HealthCheckWriter.WriteHealthStatusAsJson(report, false, false, false);

        json.Should().Contain("\"tags\"");
        json.Should().Contain("db");
        json.Should().Contain("critical");
    }

    [Fact]
    public void WriteHealthStatusAsJson_WritesData()
    {
        var entry = new HealthReportEntry(
            HealthStatus.Healthy,
            "ok",
            TimeSpan.Zero,
            exception: null,
            data: new Dictionary<string, object>
            {
                ["connections"] = 5,
                ["region"] = "eu-west-2"
            },
            tags: null);

        var report = new HealthReport(
            new Dictionary<string, HealthReportEntry> { ["db"] = entry },
            TimeSpan.Zero);

        var json = HealthCheckWriter.WriteHealthStatusAsJson(report, false, false, false);

        json.Should().Contain("\"data\"");
        json.Should().Contain("\"connections\":5");
        json.Should().Contain("\"region\":\"eu-west-2\"");
    }

    [Fact]
    public void WriteHealthStatusAsJson_WritesExceptionInfo()
    {
        var inner = new InvalidOperationException("inner");
        var ex = new Exception("outer", inner);

        var entry = new HealthReportEntry(
            HealthStatus.Unhealthy,
            "fail",
            TimeSpan.Zero,
            exception: ex,
            data: null,
            tags: null);

        var report = new HealthReport(
            new Dictionary<string, HealthReportEntry> { ["service"] = entry },
            TimeSpan.Zero);

        var json = HealthCheckWriter.WriteHealthStatusAsJson(report, false, false, false);

        json.Should().Contain("\"exception\"");
        json.Should().Contain("Exception");
        json.Should().Contain("InvalidOperationException");
    }

    [Fact]
    public void WriteHealthStatusAsJson_WhenIndented_ProducesIndentedJson()
    {
        var report = new HealthReport(new Dictionary<string, HealthReportEntry>(), TimeSpan.Zero);

        var json = HealthCheckWriter.WriteHealthStatusAsJson(report, false, false, indented: true);

        json.Should().Contain("\n");
    }

    [Fact]
    public void WriteHealthStatusAsJson_WhenNoEntries_DoesNotWriteResults()
    {
        var report = new HealthReport(new Dictionary<string, HealthReportEntry>(), TimeSpan.Zero);

        var json = HealthCheckWriter.WriteHealthStatusAsJson(report, false, false, false);

        json.Should().NotContain("results");
    }
}