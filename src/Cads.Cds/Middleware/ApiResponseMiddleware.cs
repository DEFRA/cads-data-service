using System.Text;
using System.Text.Json;

namespace Cads.Cds.Middleware;

public class ApiResponseMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    // Cache JsonSerializerOptions to avoid CA1869
    private static readonly JsonSerializerOptions DeserializeOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private static readonly JsonSerializerOptions SerializeOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public async Task InvokeAsync(HttpContext context)
    {
        var requestId = Guid.NewGuid().ToString();
        context.Items["RequestId"] = requestId;
        var originalBodyStream = context.Response.Body;

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        await _next(context);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var bodyText = await new StreamReader(memoryStream).ReadToEndAsync();
        memoryStream.Seek(0, SeekOrigin.Begin);

        if (context.Response.ContentType != null &&
            context.Response.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase) &&
            !bodyText.TrimStart().StartsWith("{\"meta\""))
        {
            object? parsedData = null;
            try
            {
                parsedData = JsonSerializer.Deserialize<object>(bodyText, DeserializeOptions) ?? bodyText;
            }
            catch
            {
                parsedData = bodyText;
            }

            var apiResponse = new
            {
                meta = new
                {
                    status = context.Response.StatusCode,
                    message = GetDefaultMessage(context.Response.StatusCode),
                    timestamp = DateTime.UtcNow,
                    requestId = requestId
                },
                data = parsedData,
                links = new
                {
                    self = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}"
                }
            };

            var wrappedJson = JsonSerializer.Serialize(apiResponse, SerializeOptions);

            context.Response.ContentType = "application/json";
            context.Response.ContentLength = Encoding.UTF8.GetByteCount(wrappedJson);

            await originalBodyStream.WriteAsync(Encoding.UTF8.GetBytes(wrappedJson));
        }
        else
        {
            await memoryStream.CopyToAsync(originalBodyStream);
        }
    }

    private static string GetDefaultMessage(int statusCode) =>
        statusCode switch
        {
            200 => "Request successful",
            201 => "Resource created",
            400 => "Bad request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not found",
            500 => "Internal server error",
            _ => "Request processed"
        };
}
