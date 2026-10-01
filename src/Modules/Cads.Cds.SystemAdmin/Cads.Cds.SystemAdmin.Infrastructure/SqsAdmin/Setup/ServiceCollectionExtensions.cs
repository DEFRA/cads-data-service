using Cads.Cds.SystemAdmin.Application.SqsAdmin.Services;
using Cads.Cds.SystemAdmin.Core.Configuration;
using Cads.Cds.SystemAdmin.Infrastructure.SqsAdmin.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Cads.Cds.SystemAdmin.Infrastructure.SqsAdmin.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSystemAdminSqsAdminLayer(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<Dictionary<string, SqsAdminQueueOptions>>(
            config.GetSection(ModuleConfigurationSection.SqsAdminQueuesSectionName));

        services.AddScoped<ISqsAdminService, SqsAdminService>();

        return services;
    }
}