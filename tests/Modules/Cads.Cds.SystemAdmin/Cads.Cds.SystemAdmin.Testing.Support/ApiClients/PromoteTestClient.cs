using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using Cads.Cds.SystemAdmin.Core.Promotion;
using Cads.Cds.SystemAdmin.Endpoints.Promotion.Requests;
using Cads.Cds.SystemAdmin.Testing.Support.Constants;
using System.Net.Http.Json;

namespace Cads.Cds.SystemAdmin.Testing.Support.ApiClients;

public static class PromoteTestClient
{
    private const string HeaderName = "x-cdp-request-id";

    public static async Task<HttpResponseMessage> PromoteImportAsync(
        HttpClient client,
        PromotionType promotionType,
        string correlationId,
        CancellationToken cancellationToken,
        bool? deleteSource = null)
    {
        var endpoint = string.Format(TestEndpointConstants.PromoteImportEndpoint, promotionType.ToString());

        using var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
        {
            Content = JsonContent.Create(
                new PromoteImportRequest(deleteSource),
                options: JsonDefaults.DefaultOptionsWithStringEnumConversion)
        };

        request.Headers.Add(HeaderName, correlationId);

        return await client.SendAsync(request, cancellationToken);
    }

    public static async Task<T?> ReadDtoAsync<T>(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        return await response.Content.ReadFromJsonAsync<T>(
            JsonDefaults.DefaultOptionsWithStringEnumConversion,
            cancellationToken);
    }
}