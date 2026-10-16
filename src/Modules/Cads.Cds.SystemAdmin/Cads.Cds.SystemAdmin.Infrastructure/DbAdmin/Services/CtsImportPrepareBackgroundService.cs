using Cads.Cds.SystemAdmin.Application.DbAdmin.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cads.Cds.SystemAdmin.Infrastructure.DbAdmin.Services;

public class CtsImportPrepareBackgroundService(
    CtsImportPrepareJobQueue queue,
    IServiceScopeFactory scopeFactory,
    ILogger<CtsImportPrepareBackgroundService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await foreach (var job in queue.ReadAllAsync(stoppingToken))
            {
                await ProcessJobAsync(job, stoppingToken);
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            // Graceful shutdown requested.
        }
    }

    private async Task ProcessJobAsync(CtsImportPrepareJob job, CancellationToken cancellationToken)
    {
        try
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Preparing CTS parallel bulk import, requested by {User}", job.RequestedBy);
            }

            using var scope = scopeFactory.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICtsImportPrepareService>();
            var runId = await service.PrepareAsync(cancellationToken);

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("CTS parallel bulk import prepared. Run id: {RunId}", runId);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Warning))
            {
                logger.LogWarning("CTS parallel bulk import prepare cancelled by host shutdown. The transaction was rolled back; no run was created.");
            }
        }
        catch (Exception ex)
        {
            // Includes the procedure's own guards (another plan in progress, unfinished run exists).
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Failed to prepare CTS parallel bulk import requested by {User}", job.RequestedBy);
            }
        }
        finally
        {
            queue.MarkCompleted();
        }
    }
}