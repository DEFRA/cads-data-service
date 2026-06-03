using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;

public static class HttpResponseMessageUtilities
{
    public static async Task<T> VerifyBadRequest<T>(HttpResponseMessage response)
    {
        VerifyResponseIsBadRequest(response);
        var result = await response.Content.ReadFromJsonAsync<T>(TestContext.Current.CancellationToken);
        result.Should().NotBeNull();
        return result;
    }

    public static async Task<T> VerifyOk<T>(HttpResponseMessage response)
    {
        VerifyResponseIsOk(response);
        var result = await response.Content.ReadFromJsonAsync<T>(TestContext.Current.CancellationToken);
        result.Should().NotBeNull();
        return result;
    }

    private static void VerifyResponseIsOk(HttpResponseMessage? response)
    {
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private static void VerifyResponseIsBadRequest(HttpResponseMessage? response)
    {
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}