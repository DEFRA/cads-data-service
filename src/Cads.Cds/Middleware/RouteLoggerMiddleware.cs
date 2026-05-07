namespace Cads.Cds.Middleware;

public class RouteLoggerMiddleware
{
    private readonly RequestDelegate _next;

    public RouteLoggerMiddleware(RequestDelegate next, EndpointDataSource endpointDataSource, ILogger<RouteLoggerMiddleware> logger, IConfiguration cf)
    {
        foreach (var endpoint in endpointDataSource.Endpoints.OfType<RouteEndpoint>())
        {
            var methods = endpoint.Metadata
                .OfType<HttpMethodMetadata>()
                .FirstOrDefault()?.HttpMethods ?? ["ANY"];

            if (logger.IsEnabled(LogLevel.Information))
            {
                var methodsText = string.Join(",", methods);
                var routePattern = endpoint.RoutePattern?.RawText ?? string.Empty;
                logger.LogInformation("{Methods} => {RoutePattern}", methodsText, routePattern);
            }
        }

        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
    }
}