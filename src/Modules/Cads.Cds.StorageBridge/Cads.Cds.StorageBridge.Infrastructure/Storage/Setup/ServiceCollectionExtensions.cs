using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Services;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Core.Configuration;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Clients;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Configuration;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Health;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Readers;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.StorageBridge.Infrastructure.Storage.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorageBridgeStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var moduleConfig = configuration.GetSection(ModuleConfigurationSection.StorageSectionName)
            .Get<StorageBridgeStorageConfiguration>()
            ?? throw new InvalidOperationException("Missing 'StorageBridge' storage config");

        services.AddSingleton(moduleConfig);

        services.AddSingleton<IConfigureS3Clients, StorageBridgeS3Configurator>();

        services.AddScoped<IFileChecksumService, S3FileChecksumService<CadsInternalClient>>();
        // Register module storage readers
        services.AddSingleton<IStorageReader<CadsInternalClient>, BulkImportStorageReader<CadsInternalClient>>();

        // Register module storage writers
        services.AddSingleton<IStorageService<CadsInternalClient>, StorageService<CadsInternalClient>>();

        if (moduleConfig.CadsInternal.HealthcheckEnabled)
        {
            services.AddSingleton<IEnableS3HealthCheck, CadsInternalHealthCheckMarker>();
        }

        return services;
    }
}