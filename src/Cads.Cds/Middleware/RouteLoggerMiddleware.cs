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

            logger.LogInformation($"{string.Join(",", methods)} => {endpoint.RoutePattern.RawText}");
        }

        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
    }
}
