using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Logging.Enrichers;

public class HttpContextEnricher(IHttpContextAccessor accessor) : ILogEventEnricher
{
    private readonly IHttpContextAccessor _accessor = accessor;

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory factory)
    {
        var ctx = _accessor.HttpContext;
        if (ctx == null) return;

        var userAgent = ctx.Request.Headers.UserAgent.ToString();
        var clientIp = ctx.Connection.RemoteIpAddress?.ToString();

        logEvent.AddPropertyIfAbsent(factory.CreateProperty("http.user_agent", userAgent));
        logEvent.AddPropertyIfAbsent(factory.CreateProperty("client.ip", clientIp));
    }
}
