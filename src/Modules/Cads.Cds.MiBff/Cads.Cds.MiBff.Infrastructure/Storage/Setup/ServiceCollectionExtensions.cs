using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.MiBff.Application.Reports.Storage;
using Cads.Cds.MiBff.Core.Configuration;
using Cads.Cds.MiBff.Infrastructure.Storage.Configuration;
using Cads.Cds.MiBff.Infrastructure.Storage.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.MiBff.Infrastructure.Storage.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMiBffStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var moduleConfig = configuration.GetSection(ModuleConfigurationSection.StorageSectionName)
            .Get<MiBffStorageConfiguration>()
            ?? throw new InvalidOperationException("Missing 'MiBff' storage config");

        services.AddSingleton(moduleConfig);

        services.AddSingleton<IConfigureS3Clients, MiBffS3Configurator>();

        services.AddTransient<IReportFileStore, S3ReportFileStore>();

        return services;
    }
}
