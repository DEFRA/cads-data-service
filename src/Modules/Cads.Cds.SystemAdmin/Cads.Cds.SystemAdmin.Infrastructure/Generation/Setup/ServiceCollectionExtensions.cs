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
    }

    private static void RegisterUtils(this IServiceCollection services)
    {
        services.AddTransient<IBusinessKeysAllocator, BusinessKeysAllocator>();
        services.AddTransient<IFileAssembler, FileAssembler>();
        services.AddTransient<IFileNameGenerator, FileNameGenerator>();
    }
}