using Cads.Cds.MiBff.Application.Reports.Routing.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cads.Cds.MiBff.Application.Reports.Routing;

public class ReportRegistry : IReportRegistry
{
    private readonly Dictionary<string, (Type Handler, Type Request)> _handlerTypes = [];

    public ReportRegistry()
    {
        var handlers = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => !t.IsAbstract &&
                        typeof(IReportHandler).IsAssignableFrom(t))
            .Select(t => new
            {
                Type = t,
                Attr = t.GetCustomAttribute<ReportHandlerAttribute>()
            })
            .Where(x => x.Attr != null);

        foreach (var h in handlers)
        {
            _handlerTypes[h.Attr!.ReportKey] = (h.Type, h.Attr!.RequestType);
        }
    }

    public (IReportHandler Handler, Type RequestType) Resolve(string reportKey, IServiceProvider provider)
    {
        if (!_handlerTypes.TryGetValue(reportKey, out var handlerType))
            throw new KeyNotFoundException($"Unknown reportKey: {reportKey}");

        var handler = (IReportHandler)provider.GetRequiredService(handlerType.Handler);

        return (handler, handlerType.Request);
    }
}