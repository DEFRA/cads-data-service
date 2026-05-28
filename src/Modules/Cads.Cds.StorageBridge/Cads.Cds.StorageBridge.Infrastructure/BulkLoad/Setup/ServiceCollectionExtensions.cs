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
        services.AddSingleton(Channel.CreateUnbounded<CreateS3BulkLoadJobDto>(new UnboundedChannelOptions() { SingleReader = false }));

        services.AddHostedService<S3BulkLoadBackgroundService>();

        services.AddTransient<IS3BulkLoadJobEnqueuer, S3BulkLoadJobEnqueuer>();

        services.AddTransient<IS3ToPostgresCopyService, S3ToPostgresCopyService>();

        services.AddTransient<IS3SqlScriptExecutorService, S3SqlScriptExecutorService>();
        services.AddScoped<IDataSeedIngestionHistoryReadRepository, DataSeedIngestionHistoryReadRepository>();
        services.AddScoped<IDataSeedIngestionHistoryWriteRepository, DataSeedIngestionHistoryWriteRepository>();

        services.AddScoped<IS3BulkLoadCommandFactoryProvider, S3BulkLoadCommandFactoryProvider>();

        return services;
    }
}