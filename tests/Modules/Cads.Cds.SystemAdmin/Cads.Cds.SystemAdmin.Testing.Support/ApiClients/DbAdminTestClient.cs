using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Requests;
using Cads.Cds.SystemAdmin.Endpoints.DbAdmin.Responses;
using Cads.Cds.SystemAdmin.Testing.Support.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;

namespace Cads.Cds.SystemAdmin.Testing.Support.ApiClients;

public static class DbAdminTestClient
{
    public static async Task<HttpResponseMessage> ExecuteAsync(
        HttpClient client,
        string command,
        object? args,
        CancellationToken cancellationToken)
    {
        var endpoint = TestEndpointConstants.DbAdminExecuteCommandEndpoint;

        JsonElement? argsElement = args is null
            ? null
            : JsonSerializer.SerializeToElement(args, JsonDefaults.DefaultOptionsWithStringEnumConversion);

        var request = new DbAdminExecuteCommandRequest(command, argsElement);

        return await client.PostAsJsonAsync(
            endpoint,
            request,
            JsonDefaults.DefaultOptionsWithStringEnumConversion,
            cancellationToken);
    }

    public static async Task<DbAdminExecuteCommandResponse?> ReadDtoAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        return await response.Content.ReadFromJsonAsync<DbAdminExecuteCommandResponse>(
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