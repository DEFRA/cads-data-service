using Cads.Cds.StorageBridge.Application.Services;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;

namespace Cads.Cds.StorageBridge.Application.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorageBridgeApplicationLayer(this IServiceCollection services)
    {
        services.RegisterServices();

        return services;
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<Channel<CreateBulkImportJobDto>>(Channel.CreateUnbounded<CreateBulkImportJobDto>(new UnboundedChannelOptions() { SingleReader = false }));

        services.AddTransient<IBulkImportEnqueueService, BulkImportEnqueueService>();
    }
}