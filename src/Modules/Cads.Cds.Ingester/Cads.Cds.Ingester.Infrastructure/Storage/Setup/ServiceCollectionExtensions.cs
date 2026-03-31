using Cads.Cds.BuildingBlocks.Core.Storage;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.Ingester.Core.Configuration;
using Cads.Cds.Ingester.Core.Services.AnimalMovements;
using Cads.Cds.Ingester.Infrastructure.Services;
using Cads.Cds.Ingester.Infrastructure.Storage.Clients;
using Cads.Cds.Ingester.Infrastructure.Storage.Configuration;
using Cads.Cds.Ingester.Infrastructure.Storage.Health;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.Ingester.Infrastructure.Storage.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIngesterStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var moduleConfig = configuration.GetSection(ModuleConfigurationSection.StorageSectionName)
            .Get<IngesterStorageConfiguration>()
            ?? throw new InvalidOperationException("Missing 'Ingester' storage config");

        services.AddSingleton(moduleConfig);

        services.AddSingleton<IConfigureS3Clients, IngesterS3Configurator>();

        // Register module storage writers
        services.AddSingleton<IStorageWriter, IngesterStorageWriter>();
        services.AddTransient<IIngesterStorageService, IngesterStorageService>();

        // Register module storage readers

        if (moduleConfig.CadsIngester.HealthcheckEnabled)
        {
            services.AddSingleton<IEnableS3HealthCheck, CadsIngesterHealthCheckMarker>();
        }

        return services;
    }
}