using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;
using Cads.Cds.StorageBridge.Core.Domain.Repositories;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Repositories;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureBulkLoadServices(this IServiceCollection services)
    {
        services.AddSingleton(Channel.CreateUnbounded<CreateS3CsvBulkLoadJobDto>(new UnboundedChannelOptions() { SingleReader = false }));
        services.AddSingleton(Channel.CreateUnbounded<CreateS3SqlImportJobDto>(new UnboundedChannelOptions() { SingleReader = false }));

        services.AddHostedService<S3CsvBulkLoadBackgroundService>();
        services.AddHostedService<S3SqlBulkLoadBackgroundService>();

        services.AddTransient<IS3BulkLoadJobEnqueuer<CreateS3CsvBulkLoadJobDto>, S3BulkLoadJobEnqueuer<CreateS3CsvBulkLoadJobDto>>();
        services.AddTransient<IS3BulkLoadJobEnqueuer<CreateS3SqlImportJobDto>, S3BulkLoadJobEnqueuer<CreateS3SqlImportJobDto>>();

        services.AddTransient<IS3ToPostgresCopyService, S3ToPostgresCopyService>();
        services.AddTransient<IS3SqlScriptExecutorService, S3SqlScriptExecutorService>();

        services.AddScoped<IDataSeedIngestionHistoryRepository, DataSeedIngestionHistoryRepository>();
        services.AddSingleton<IS3BulkLoadCommandFactoryProvider, S3BulkLoadCommandFactoryProvider>();

        return services;
    }
}