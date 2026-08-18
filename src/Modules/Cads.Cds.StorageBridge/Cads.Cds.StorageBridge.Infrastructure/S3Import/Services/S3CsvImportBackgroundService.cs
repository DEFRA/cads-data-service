using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Core.Correlation;
using Cads.Cds.BuildingBlocks.Core.DTOs;
using Cads.Cds.StorageBridge.Application.Imports.Repositories;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;

public class S3CsvImportBackgroundService(
    Channel<CreateS3CsvImportJobDto> channel,
    IServiceScopeFactory serviceScopeFactory,
    ILogger<S3CsvImportBackgroundService> logger,
    IS3ToPostgresCopyService processor
) : S3ImportBackgroundService<CreateS3CsvImportJobDto>(channel, logger, processor)
{
    protected override async Task ProcessJobAsync(
        CreateS3CsvImportJobDto request,
        SemaphoreSlim semaphore,
        CancellationToken cancellationToken)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();

        using (CorrelationScope.Begin(request.CorrelationId))
        {
            try
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<StorageBridgeWriteDbContext>();
                var fileImportRepository = scope.ServiceProvider.GetRequiredService<IStorageBridgeFileImportRepository>();

                var fileImport = await fileImportRepository.GetByIdAsync(request.FileImportId, cancellationToken);

                try
                {
                    await processor.ExecuteAsync(request, cancellationToken);

                    fileImport!.SetImportStatus(FileImportStatus.Completed);
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
                catch (Exception)
                {
                    fileImport!.SetImportStatus(FileImportStatus.Failed);
                    await dbContext.SaveChangesAsync(cancellationToken);

                    throw;
                }
                finally
                {
                    semaphore.Release();
                }
            }
            catch (Exception ex)
            {
                if (logger.IsEnabled(LogLevel.Error))
                {
                    logger.LogError(ex, "Failed to process bulk load job {JobId}", request.JobId);
                }
            }
        }
    }
}