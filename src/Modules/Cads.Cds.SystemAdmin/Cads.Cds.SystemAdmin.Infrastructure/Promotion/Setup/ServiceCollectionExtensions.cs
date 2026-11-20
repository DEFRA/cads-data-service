using Cads.Cds.SystemAdmin.Application.Promotion.Channels;
using Cads.Cds.SystemAdmin.Application.Promotion.Factories;
using Cads.Cds.SystemAdmin.Core.Configuration;
using Cads.Cds.SystemAdmin.Core.Promotion;
using Cads.Cds.SystemAdmin.Infrastructure.Promotion.Channels;
using Cads.Cds.SystemAdmin.Infrastructure.Promotion.Factories;
using Cads.Cds.SystemAdmin.Infrastructure.Promotion.Factories.Strategies;
using Cads.Cds.SystemAdmin.Infrastructure.Promotion.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Promotion.Setup;

public static class ServiceCollectionExtensions
{
    public static void AddSystemAdminPromotion(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<PromotionOptions>()
            .Bind(configuration.GetSection(ModuleConfigurationSection.PromotionSectionName))
            .Validate(o => o.PromotionTypes is { Count: > 0 }, "Promotion:PromotionTypes must be configured.")
            .ValidateOnStart();

        services.RegisterJobQueues();
        services.RegisterBackgroundServices();
        services.RegisterStrategyFactories();
    }

    public static void RegisterJobQueues(this IServiceCollection services)
    {
        services.AddSingleton<PromotionJobQueue>();
        services.AddSingleton<IPromotionJobQueue>(sp => sp.GetRequiredService<PromotionJobQueue>());
    }

    public static void RegisterBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<PromotionJobBackgroundService>();
    }

    public static void RegisterStrategyFactories(this IServiceCollection services)
    {
        services.AddTransient<IPromotionJobProcessorStrategyFactory, PromotionJobProcessorStrategyFactory>();

        services.AddTransient<IPromotionJobProcessorStrategy, CtsBulkPromotionStrategy>();
        services.AddTransient<IPromotionJobProcessorStrategy, CtsDeltaPromotionStrategy>();
    }
}