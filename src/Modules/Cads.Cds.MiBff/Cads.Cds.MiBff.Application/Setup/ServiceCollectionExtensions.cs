using Cads.Cds.MiBff.Application.Configuration;
using Cads.Cds.MiBff.Application.Queries.Holdings.Adapters;
using Cads.Cds.MiBff.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cads.Cds.MiBff.Application.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMiBffApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(IRequestExecutor).Assembly);
        });

        services.AddScoped<IRequestExecutor, RequestExecutor>();
        services.Configure<MiBffConfig>(configuration.GetSection(MiBffConfig.SectionName));

        services.RegisterValidationConfig(configuration);

        services.RegisterAdapters();
        services.RegisterServices(Assembly.GetExecutingAssembly());

        return services;
    }

    public static void RegisterAdapters(this IServiceCollection services)
    {
        services.AddScoped<HoldingsQueryAdapter>();
        services.AddScoped<HoldingsQueryByCphAdapter>();
    }

    public static void RegisterServices(this IServiceCollection services, Assembly assembly)
    {
        services.AddTransient<IHoldingsService, HoldingsService>();
    }

    /// <summary>
    /// Register strongly-typed config for each query validator.
    /// </summary>
    /// <remarks>Each validator constructor can request a strongly typed config for its own particular defaults
    /// (e.g. GetSiteQueryValidator constructor takes parameter of type QueryValidationConfig<GetSiteQueryValidator>)</remarks>
    private static void RegisterValidationConfig(this IServiceCollection services, IConfiguration configuration)
    {
        //var queryValidationConfig = configuration.GetSection(QueryValidationConfig.SectionName).Get<List<QueryValidationConfig>>();
        //var validatorTypes = typeof(Queries.Sites.GetSitesQueryValidator).Assembly.GetTypes();
        //var getConfigSectionMethod = typeof(ConfigurationBinder)
        //    .GetMethods()
        //    .Single(m => m.Name == "Get"
        //        && m.ContainsGenericParameters
        //        && m.GetParameters().Length == 1
        //        && m.GetParameters().Single().ParameterType == typeof(IConfiguration));

        //for (var i = 0; i < queryValidationConfig?.Count; i++)
        //{
        //    var validatorType = validatorTypes.Single(t => t.Name == queryValidationConfig[i].ValidatorType);
        //    var typeOfConfigForValidator = typeof(QueryValidationConfig<>).MakeGenericType(validatorType);
        //    var configInstance = getConfigSectionMethod
        //        .MakeGenericMethod(typeOfConfigForValidator)
        //        .Invoke(null, [configuration.GetSection($"{QueryValidationConfig.SectionName}:{i}")]);
        //    services.AddSingleton(typeOfConfigForValidator, configInstance!);
        //}
    }
}