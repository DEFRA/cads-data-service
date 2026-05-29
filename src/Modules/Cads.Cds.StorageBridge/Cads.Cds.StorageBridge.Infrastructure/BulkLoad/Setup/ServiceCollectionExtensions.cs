using Cads.Cds.StorageBridge.Application.BulkLoad.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Factories;
using Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Infrastructure.BulkLoad.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureBulkLoadServices(this IServiceCollection services)
    {
        services.AddSingleton(Channel.CreateUnbounded<CreateS3BulkLoadJobDto>(new UnboundedChannelOptions() { SingleReader = false }));

        services.AddHostedService<S3CsvBulkLoadBackgroundService>();
        services.AddHostedService<S3SqlBulkLoadBackgroundService>();

        services.AddTransient<IS3BulkLoadJobEnqueuer<CreateS3CsvBulkLoadJobDto>, S3BulkLoadJobEnqueuer<CreateS3CsvBulkLoadJobDto>>();
        services.AddTransient<IS3BulkLoadJobEnqueuer<CreateS3SqlImportJobDto>, S3BulkLoadJobEnqueuer<CreateS3SqlImportJobDto>>();

        services.AddTransient<IS3ToPostgresCopyService, S3ToPostgresCopyService>();

        services.AddScoped<IS3BulkLoadCommandFactoryProvider, S3BulkLoadCommandFactoryProvider>();

        return services;
    }
}