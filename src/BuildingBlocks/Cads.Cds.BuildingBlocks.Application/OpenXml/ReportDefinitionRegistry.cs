using Cads.Cds.BuildingBlocks.Core.OpenXml;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cads.Cds.BuildingBlocks.Application.OpenXml;

public class ReportDefinitionRegistry : IReportDefinitionRegistry
{
    private readonly Dictionary<Type, Type> _definitions = [];
    private readonly IServiceProvider _provider;

    public ReportDefinitionRegistry(IServiceProvider provider, IEnumerable<Assembly> assemblies)
    {
        _provider = provider;

        foreach (var assembly in assemblies)
        {
            RegisterDefinitionsFromAssembly(assembly);
        }
    }

    private void RegisterDefinitionsFromAssembly(Assembly assembly)
    {
        var definitionTypes = assembly
            .GetTypes()
            .Where(t => !t.IsAbstract &&
                        t.GetInterfaces().Any(i =>
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IReportDefinition<>)));

        foreach (var type in definitionTypes)
        {
            var iface = type.GetInterfaces()
                .Single(i => i.IsGenericType &&
                             i.GetGenericTypeDefinition() == typeof(IReportDefinition<>));

            var modelType = iface.GetGenericArguments()[0];

            if (_definitions.ContainsKey(modelType))
                throw new InvalidOperationException(
                    $"Multiple report definitions found for type {modelType.Name}");

            _definitions[modelType] = type;
        }
    }

    public IReportDefinition<T> GetDefinition<T>()
    {
        if (!_definitions.TryGetValue(typeof(T), out _))
            throw new InvalidOperationException(
                $"No report definition registered for type {typeof(T).Name}");

        var serviceType = typeof(IReportDefinition<>).MakeGenericType(typeof(T));
        return (IReportDefinition<T>)_provider.GetRequiredService(serviceType);
    }
}