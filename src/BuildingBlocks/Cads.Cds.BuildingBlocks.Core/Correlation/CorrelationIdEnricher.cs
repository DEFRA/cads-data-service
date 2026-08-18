using Serilog.Core;
using Serilog.Events;

namespace Cads.Cds.BuildingBlocks.Core.Correlation;

public class CorrelationIdEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var correlationId = CorrelationIdContext.Value;

        if (string.IsNullOrWhiteSpace(correlationId))
            return;

        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CorrelationId", correlationId));
    }
}