using Cads.Cds.SystemAdmin.Application.Generation.Dispatchers;
using Cads.Cds.SystemAdmin.Application.Generation.Scenarios;
using Cads.Cds.SystemAdmin.Application.Generation.Utils;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Dispatchers;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Scenarios.CtLocationScenarios;
using Cads.Cds.SystemAdmin.Infrastructure.Generation.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSystemAdminGeneration(this IServiceCollection services)
    {
        services.RegisterUtils();
        services.RegisterScenarios();
        services.RedisterDispatchers();

        return services;
    }

    private static void RedisterDispatchers(this IServiceCollection services)
    {
        services.AddTransient<IScenarioGenerationDispatcher, ScenarioGenerationDispatcher>();
    }

    private static void RegisterScenarios(this IServiceCollection services)
    {
        services.AddTransient<IGenerationScenario, CtLocationBulkScenario>();
        services.AddTransient<IGenerationScenario, CtLocationUpdateScenario>();

        //services.RegisterAllImplementations<IGenerationScenario>();
    }

    private static void RegisterUtils(this IServiceCollection services)
    {
        services.AddTransient<IBusinessKeysAllocator, BusinessKeysAllocator>();
        services.AddTransient<IFileAssembler, FileAssembler>();
        services.AddTransient<IFileNameGenerator, FileNameGenerator>();
    }

    /// <summary>
    /// Registers all non-abstract classes implementing TInterface found in the current assembly.
    /// </summary>
    private static void RegisterAllImplementations<TInterface>(this IServiceCollection services)
    {
        var interfaceType = typeof(TInterface);

        // Scan all loaded assemblies (you can limit to specific assemblies if needed)
        var types = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => interfaceType.IsAssignableFrom(t) &&
                        t.IsClass &&
                        !t.IsAbstract);

        foreach (var type in types)
        {
            services.AddTransient(interfaceType, type);
        }
    }
}