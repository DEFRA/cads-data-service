using Cads.Cds.BuildingBlocks.Application.Attributes;
using Cads.Cds.BuildingBlocks.Application.Queries.JsonResponses;
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
        if (!ShouldWrap(context))
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

        if (!ShouldProcessJson(context, bodyText))
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.CopyToAsync(originalBodyStream);
            return;
        }

        var dataEnvelope = ParseDataEnvelope(bodyText)
            ?? WrapRawPayload(bodyText);

        ApplyMessageAttributes(context, dataEnvelope);
        EnsureParameters(context, dataEnvelope);

        var apiResponse = BuildResponse(context, dataEnvelope);
        var wrappedJson = JsonSerializer.Serialize(apiResponse, JsonDefaults.DefaultOptionsWithIndented);

        context.Response.ContentType = "application/json";
        context.Response.ContentLength = Encoding.UTF8.GetByteCount(wrappedJson);

        await originalBodyStream.WriteAsync(Encoding.UTF8.GetBytes(wrappedJson));
    }

    private static bool ShouldWrap(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        return endpoint?.Metadata.GetMetadata<ResponseWithMetaDataAttribute>() != null;
    }

    private static bool ShouldProcessJson(HttpContext context, string body)
    {
        var isJson = context.Response.ContentType?.Contains("application/json", StringComparison.OrdinalIgnoreCase) == true;
        var alreadyWrapped = body.TrimStart().StartsWith("{\"meta\"", StringComparison.Ordinal);
        return isJson && !alreadyWrapped;
    }

    private static JsonResponseData<object>? ParseDataEnvelope(string body)
    {
        try
        {
            return JsonSerializer.Deserialize<JsonResponseData<object>>(body, JsonDefaults.DefaultOptions);
        }
        catch
        {
            return null;
        }
    }

    private static JsonResponseData<object> WrapRawPayload(string body)
    {
        object? raw;

        try
        {
            raw = JsonSerializer.Deserialize<object>(body, JsonDefaults.DefaultOptions) ?? body;
        }
        catch
        {
            raw = body;
        }

        return new JsonResponseData<object>
        {
            Results = raw is IEnumerable<object> list ? list : [raw]
        };
    }

    private static void ApplyMessageAttributes(HttpContext context, JsonResponseData<object> envelope)
    {
        var endpoint = context.GetEndpoint();
        var attr = endpoint?.Metadata.GetMetadata<ApiMessageAttribute>();

        if (attr != null)
        {
            envelope.Message = attr.Message;
            envelope.Description = attr.Description;
        }
    }

    private static void EnsureParameters(HttpContext context, JsonResponseData<object> envelope)
    {
        envelope.Parameters ??= new JsonResponseDataParameters
        {
            Path = context.Request.Path,
            Query = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : null
        };
    }

    private static JsonResponseWithMetaData BuildResponse(HttpContext context, JsonResponseData<object> envelope)
    {
        return new JsonResponseWithMetaData
        {
            Meta = new JsonResponseMetaData
            {
                Service = string.Empty,
                Version = string.Empty,
                RequestId = CorrelationIdContext.Value,
                Timestamp = DateTime.UtcNow,
                Endpoint = context.Request.Path,
                Status = GetDefaultMessage(context.Response.StatusCode)
            },
            Data = envelope,
            Links = new JsonResponseLinks
            {
                Self = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}"
            }
        };
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