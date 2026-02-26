using Cads.Cds.ApiSurface.Dtos.Common.JsonResponsesWrap;
using Cads.Cds.BuildingBlocks.Application.Attributes;
using Cads.Cds.BuildingBlocks.Core.Correlation;
using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using System.Text;
using System.Text.Json;

namespace Cads.Cds.Middleware;

public class ApiResponseMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var shouldWrap = endpoint?.Metadata.GetMetadata<ResponseWithMetaDataAttribute>() != null;

        if (!shouldWrap)
        {
            await _next(context);
            return;
        }

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
                parsedData = JsonSerializer.Deserialize<object>(bodyText, JsonDefaults.DefaultOptions) ?? bodyText;
            }
            catch
            {
                parsedData = bodyText;
            }

            var apiResponse = new JsonResponseWithMetaData
            {
                Meta = new JsonResponseMetaData
                {
                    Service = string.Empty,
                    Version = string.Empty,
                    RequestId = CorrelationIdContext.Value,
                    Timestamp = DateTime.UtcNow,
                    Endpoint = string.Empty,
                    Status = GetDefaultMessage(context.Response.StatusCode)
                },
                Data = parsedData,
                Links = new JsonResponseLinks
                {
                    Self = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}"
                }
            };

            var wrappedJson = JsonSerializer.Serialize(apiResponse, JsonDefaults.DefaultOptionsWithIndented);

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