using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using Cads.Cds.SystemAdmin.Core.DTOs.Generation;
using Cads.Cds.SystemAdmin.Testing.Support.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using Cads.Cds.SystemAdmin.Endpoints.Generation.Requests;

namespace Cads.Cds.SystemAdmin.Testing.Support.ApiClients;

public static class GenerationTestClient
{
    public static async Task<HttpResponseMessage> CreateAsync(
        HttpClient client,
        CreateGenerationRequest? request,
        CancellationToken cancellationToken)
    {
        var endpoint = TestEndpointConstants.GenerationCreateEndpoint;
        return await client.PostAsJsonAsync(endpoint, request, cancellationToken);
    }

    public static async Task<HttpResponseMessage> GetScenarios(
        HttpClient client,
        CancellationToken cancellationToken)
    {
        var endpoint = TestEndpointConstants.GenerationGetScenariosEndpoint;
        return await client.GetAsync(endpoint, cancellationToken);
    }

    public static async Task<CreateGenerationResponseDto?> ReadDtoAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        return await ReadDtoAsync<CreateGenerationResponseDto>(response,
            cancellationToken);
    }

    public static async Task<T?> ReadDtoAsync<T>(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        return await response.Content.ReadFromJsonAsync<T>(
            JsonDefaults.DefaultOptionsWithStringEnumConversion,
            cancellationToken);
    }

    public static async Task<ProblemDetails?> ReadProblemDetailsAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        return await response.Content.ReadFromJsonAsync<ProblemDetails>(
            JsonDefaults.DefaultOptionsWithStringEnumConversion,
            cancellationToken);
    }
}