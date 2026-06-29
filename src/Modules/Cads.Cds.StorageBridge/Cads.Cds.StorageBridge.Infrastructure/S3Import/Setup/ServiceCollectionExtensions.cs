using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.Domain.Repositories;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Repositories;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.S3Import.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureBulkLoadServices(this IServiceCollection services)
    {
        services.AddSingleton(Channel.CreateUnbounded<CreateS3CsvImportJobDto>(new UnboundedChannelOptions() { SingleReader = false }));
        services.AddSingleton(Channel.CreateUnbounded<CreateS3SqlImportJobDto>(new UnboundedChannelOptions() { SingleReader = false }));

        services.AddHostedService<S3CsvImportBackgroundService>();
        services.AddHostedService<S3SqlImportBackgroundService>();

        services.AddTransient<IS3ImportJobEnqueuer<CreateS3CsvImportJobDto>, S3ImportJobEnqueuer<CreateS3CsvImportJobDto>>();
        services.AddTransient<IS3ImportJobEnqueuer<CreateS3SqlImportJobDto>, S3ImportJobEnqueuer<CreateS3SqlImportJobDto>>();

        services.AddTransient<IS3ToPostgresCopyService, S3ToPostgresCopyService>();
        services.AddTransient<IS3SqlScriptExecutorService, S3SqlScriptExecutorService>();

        services.AddScoped<IDataSeedIngestionHistoryRepository, DataSeedIngestionHistoryRepository>();
        services.AddScoped<IS3ImportCommandFactoryProvider, S3ImportCommandFactoryProvider>();

        return services;
    }
}