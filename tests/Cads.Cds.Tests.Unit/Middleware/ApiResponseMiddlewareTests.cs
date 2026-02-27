using Cads.Cds.BuildingBlocks.Application.Attributes;
using Cads.Cds.Middleware;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Cads.Cds.Tests.Unit.Middleware;

public class ApiResponseMiddlewareTests
{
    private static (HttpContext Context, MemoryStream OriginalStream) CreateContext(
        string? body = null,
        string contentType = "application/json",
        bool addAttribute = true,
        ApiMessageAttribute? msgAttr = null)
    {
        var context = new DefaultHttpContext();

        var originalStream = new MemoryStream();
        context.Response.Body = originalStream;
        context.Response.ContentType = contentType;

        var metadata = new List<object>();

        if (addAttribute)
            metadata.Add(new ResponseWithMetaDataAttribute());

        if (msgAttr != null)
            metadata.Add(msgAttr);

        var endpoint = new Endpoint(
            requestDelegate: _ => Task.CompletedTask,
            metadata: new EndpointMetadataCollection([.. metadata]),
            displayName: "test");

        context.SetEndpoint(endpoint);

        if (body != null)
        {
            var bytes = Encoding.UTF8.GetBytes(body);
            originalStream.Write(bytes, 0, bytes.Length);
            originalStream.Seek(0, SeekOrigin.Begin);
        }

        return (context, originalStream);
    }

    private static string ReadBody(MemoryStream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(stream, leaveOpen: true);
        return reader.ReadToEnd();
    }

    private static RequestDelegate FakeNext(string responseBody)
    {
        return async ctx =>
        {
            var bytes = Encoding.UTF8.GetBytes(responseBody);
            await ctx.Response.Body.WriteAsync(bytes);
        };
    }

    [Fact]
    public async Task Should_not_wrap_when_attribute_missing()
    {
        var (context, originalStream) = CreateContext(
            body: "{\"x\":1}",
            addAttribute: false
        );

        var middleware = new ApiResponseMiddleware(FakeNext("{\"x\":1}"));

        await middleware.InvokeAsync(context);

        var json = ReadBody(originalStream);
        json.Should().Be("{\"x\":1}");
    }

    [Fact]
    public async Task Should_wrap_valid_json_into_envelope()
    {
        var (context, originalStream) = CreateContext(body: "{\"results\":[1,2]}");

        var middleware = new ApiResponseMiddleware(FakeNext("{\"results\":[1,2]}"));

        await middleware.InvokeAsync(context);

        var json = ReadBody(originalStream);

        json.Should().MatchRegex("\"meta\"\\s*:");
        json.Should().MatchRegex("\"data\"\\s*:");
        json.Should().MatchRegex("\"results\"\\s*:\\s*\\[");
    }

    [Fact]
    public async Task Should_not_wrap_when_already_wrapped()
    {
        var wrapped = "{\"meta\":{\"x\":1},\"data\":{\"results\":[]}}";

        var (context, originalStream) = CreateContext(body: wrapped);

        var middleware = new ApiResponseMiddleware(FakeNext(wrapped));

        await middleware.InvokeAsync(context);

        var json = ReadBody(originalStream);
        json.Should().Be(wrapped);
    }

    [Fact]
    public async Task Should_wrap_raw_json_into_results_array()
    {
        var (context, originalStream) = CreateContext(body: "{\"a\":123}");

        var middleware = new ApiResponseMiddleware(FakeNext("{\"a\":123}"));

        await middleware.InvokeAsync(context);

        var json = ReadBody(originalStream);

        json.Should().MatchRegex("\"results\"\\s*:\\s*\\[");
    }

    [Fact]
    public async Task Should_apply_message_attribute()
    {
        var msgAttr = new ApiMessageAttribute("Test message", "Test description");

        var (context, originalStream) = CreateContext(
            body: "{\"results\":[]}",
            msgAttr: msgAttr
        );

        var middleware = new ApiResponseMiddleware(FakeNext("{\"results\":[]}"));

        await middleware.InvokeAsync(context);

        var json = ReadBody(originalStream);

        json.Should().MatchRegex("\"message\"\\s*:\\s*\"Test message\"");
        json.Should().MatchRegex("\"description\"\\s*:\\s*\"Test description\"");
    }

    [Fact]
    public async Task Should_fill_parameters_when_missing()
    {
        var (context, originalStream) = CreateContext(body: "{\"results\":[]}");

        context.Request.Path = "/test";
        context.Request.QueryString = new QueryString("?x=1");

        var middleware = new ApiResponseMiddleware(FakeNext("{\"results\":[]}"));

        await middleware.InvokeAsync(context);

        var json = ReadBody(originalStream);

        json.Should().MatchRegex("\"path\"\\s*:\\s*\"/test\"");
        json.Should().MatchRegex("\"query\"\\s*:\\s*\"\\?x=1\"");
    }

    [Theory]
    [InlineData(200, "Request successful")]
    [InlineData(201, "Resource created")]
    [InlineData(400, "Bad request")]
    [InlineData(401, "Unauthorized")]
    [InlineData(403, "Forbidden")]
    [InlineData(404, "Not found")]
    [InlineData(500, "Internal server error")]
    [InlineData(999, "Request processed")]
    public async Task Should_set_correct_status_message(int status, string expected)
    {
        var (context, originalStream) = CreateContext(body: "{\"results\":[]}");

        context.Response.StatusCode = status;

        var middleware = new ApiResponseMiddleware(FakeNext("{\"results\":[]}"));

        await middleware.InvokeAsync(context);

        var json = ReadBody(originalStream);

        json.Should().Contain(expected);
    }
}