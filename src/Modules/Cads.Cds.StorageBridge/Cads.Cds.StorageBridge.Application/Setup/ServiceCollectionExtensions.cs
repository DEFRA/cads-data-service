using Cads.Cds.StorageBridge.Application.Services;
using Cads.Cds.StorageBridge.Core.Services;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddTransient<IBulkImportEnqueueService, BulkImportEnqueueService>();
    }
}