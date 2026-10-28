using Cads.Cds.Api.Setup;
using Cads.Cds.BuildingBlocks.Core.Correlation;
using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.Middleware;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Scalar.AspNetCore;
using System.Diagnostics.CodeAnalysis;

namespace Cads.Cds.Setup;

public static class WebApplicationExtensions
{
    [ExcludeFromCodeCoverage]
    public static void ConfigureRequestPipeline(this WebApplication app)
    {
        var env = app.Services.GetRequiredService<IWebHostEnvironment>();
        var applicationLifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        var configuration = app.Services.GetRequiredService<IConfiguration>();
        var healthcheckMaskingEnabled = configuration.GetValue<bool>("HealthcheckMaskingEnabled");
        var prometheusScrapingEnabled = configuration.GetValue<bool>("PrometheusScrapingEnabled");

        if (logger.IsEnabled(LogLevel.Information))
        {
            applicationLifetime.ApplicationStarted.Register(() =>
                logger.LogInformation("{ApplicationName} started", env.ApplicationName));
            applicationLifetime.ApplicationStopping.Register(() =>
                logger.LogInformation("{ApplicationName} stopping", env.ApplicationName));
            applicationLifetime.ApplicationStopped.Register(() =>
                logger.LogInformation("{ApplicationName} stopped", env.ApplicationName));
        }

        if (prometheusScrapingEnabled)
        {
            app.MapPrometheusScrapingEndpoint("/metrics");
        }

        app.UseHeaderPropagation();
        app.UseRouting();

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseCorrelationId();
        app.UseMiddleware<ApiResponseMiddleware>();

        _ = app.MapOpenApi("/openapi/{documentName}.json")
            .AllowAnonymous();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.MapModules();

        app.UseApiSoapEndpoints();

        app.MapGet("/", () => "OK").AllowAnonymous();

        // Liveness only: used by the platform container health check.
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => false
        }).AllowAnonymous();

        // Diagnostics: runs every registered check. Requires an API key.
        app.MapHealthChecks("/health/details", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = (context, healthReport) =>
            {
                context.Response.ContentType = "application/json; charset=utf-8";
                return context.Response.WriteAsync(HealthCheckWriter.WriteHealthStatusAsJson(
                    healthReport,
                    healthcheckMaskingEnabled: healthcheckMaskingEnabled,
                    excludeHealthy: false,
                    indented: true));
            },
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            }
        }).RequireAuthorization(AuthenticationConstants.DefaultPolicy);

        if (app.Environment.IsDevelopment())
        {
            _ = app.MapScalarApiReference(options =>
            {
                options.Title = "CADS CDS Documentation";
                options.Theme = ScalarTheme.Default;

                var apiVersionDescriptions = app.DescribeApiVersions();

                foreach (var description in apiVersionDescriptions)
                {
                    options.AddDocument(
                        description.GroupName,
                        $"/openapi/{description.GroupName}.json");
                }
            }).AllowAnonymous();
        }
    }
}