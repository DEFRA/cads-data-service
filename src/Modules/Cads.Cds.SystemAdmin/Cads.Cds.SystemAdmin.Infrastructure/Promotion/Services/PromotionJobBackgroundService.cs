using Cads.Cds.SystemAdmin.Application.Promotion.Factories;
using Cads.Cds.SystemAdmin.Application.Promotion.Jobs;
using Cads.Cds.SystemAdmin.Core.Promotion;
using Cads.Cds.SystemAdmin.Infrastructure.Promotion.Channels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cads.Cds.SystemAdmin.Infrastructure.Promotion.Services;

public class PromotionJobBackgroundService(
    PromotionJobQueue queue,
    IOptions<PromotionOptions> promotionOptions,
    IServiceScopeFactory scopeFactory,
    ILogger<PromotionJobBackgroundService> logger) : BackgroundService
{
    private readonly PromotionOptions _promotionOptions = promotionOptions.Value;

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

    private async Task ProcessJobAsync(PromotionJob job, CancellationToken cancellationToken)
    {
        if (!_promotionOptions.IsEnabled(job.PromotionType))
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("PromotionType: {PromotionType} for parallel import is not active", job.PromotionType);
            }
            throw new InvalidOperationException($"PromotionType: {job.PromotionType} for parallel import is not active");
        }

        try
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Preparing parallel import run for PromotionType: {PromotionType}, requested by {User}", job.PromotionType, job.RequestedBy);
            }

            using var scope = scopeFactory.CreateScope();
            var strategyFactory = scope.ServiceProvider.GetRequiredService<IPromotionJobProcessorStrategyFactory>();
            var strategy = strategyFactory.GetStrategy(job.PromotionType);
            var runId = await strategy.PrepareAsync(job, cancellationToken);

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Parallel import run for PromotionType: {PromotionType} prepared wuth RunId: {RunId}", job.PromotionType, runId);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Warning))
            {
                logger.LogWarning("Parallel import run preparation cancelled by host shutdown. The transaction was rolled back; no run was created.");
            }
        }
        catch (Exception ex)
        {
            // Includes the procedure's own guards (another plan in progress, unfinished run exists).
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Failed to prepare parallel import run for PromotionType: {PromotionType} requested by {User}", job.PromotionType, job.RequestedBy);
            }
        }
        finally
        {
            queue.MarkCompleted();
        }
    }
}
