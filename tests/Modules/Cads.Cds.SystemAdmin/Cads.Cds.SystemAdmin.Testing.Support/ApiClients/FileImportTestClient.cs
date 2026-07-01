using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using Cads.Cds.SystemAdmin.Controllers.Requests.Imports;
using Cads.Cds.SystemAdmin.Core.DTOs.Imports;
using Cads.Cds.SystemAdmin.Testing.Support.Constants;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Web;

namespace Cads.Cds.SystemAdmin.Testing.Support.ApiClients;

public static class FileImportTestClient
{
    public static async Task<HttpResponseMessage> GetByFileNameAsync(
        HttpClient client,
        string? fileName,
        CancellationToken cancellationToken)
    {
        var endpoint = TestEndpointConstants.FileImportsGetByFileNameEndpoint;

        if (!string.IsNullOrWhiteSpace(fileName))
        {
            endpoint += "?fileName=" + HttpUtility.UrlEncode(fileName);
        }

        return await client.GetAsync(endpoint, cancellationToken);
    }

    public static async Task<HttpResponseMessage> CreateAsync(
        HttpClient client,
        CreateFileImportRequest? request,
        CancellationToken cancellationToken)
    {
        var endpoint = TestEndpointConstants.FileImportsCreateEndpoint;
        return await client.PostAsJsonAsync(endpoint, request, cancellationToken);
    }

    public static async Task<HttpResponseMessage> MarkImportingAsync(
        HttpClient client,
        long id,
        CancellationToken cancellationToken)
    {
        var endpoint = string.Format(TestEndpointConstants.FileImportsImportingEndpoint, id);
        return await client.PostAsync(endpoint, null, cancellationToken);
    }

    public static async Task<HttpResponseMessage> MarkImportCompleteAsync(
        HttpClient client,
        long id,
        CancellationToken cancellationToken)
    {
        var endpoint = string.Format(TestEndpointConstants.FileImportsCompleteEndpoint, id);
        return await client.PostAsync(endpoint, null, cancellationToken);
    }

    public static async Task<HttpResponseMessage> MarkImportFailedAsync(
        HttpClient client,
        long id,
        CancellationToken cancellationToken)
    {
        var endpoint = string.Format(TestEndpointConstants.FileImportsFailedEndpoint, id);
        return await client.PostAsync(endpoint, null, cancellationToken);
    }

    public static async Task<HttpResponseMessage> ResetAsync(
        HttpClient client,
        long id,
        CancellationToken cancellationToken)
    {
        var endpoint = string.Format(TestEndpointConstants.FileImportsResetEndpoint, id);
        return await client.PostAsync(endpoint, null, cancellationToken);
    }

    public static async Task<long> GetIdByFileNameAsync(
        HttpClient client,
        string fileName,
        CancellationToken cancellationToken)
    {
        var dto = await GetAndReadAsync(client, fileName, cancellationToken);
        return dto.Id;
    }

    public static async Task<FileImportDto> GetAndReadAsync(
        HttpClient client,
        string fileName,
        CancellationToken cancellationToken)
    {
        var response = await GetByFileNameAsync(client, fileName, cancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue(
            $"GET /file-imports?fileName={fileName} should return 200 OK");

        var dto = await ReadDtoAsync(response, cancellationToken);
        dto.Should().NotBeNull($"GET /file-imports returned no DTO for fileName={fileName}");

        return dto;
    }

    public static async Task<FileImportDto?> ReadDtoAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        return await response.Content.ReadFromJsonAsync<FileImportDto>(
            JsonDefaults.DefaultOptionsWithStringEnumConversion,
            cancellationToken);
    }

    public static async Task<FileImportDto> VerifyFileImportAsync(
        HttpClient client,
        string fileName,
        CancellationToken cancellationToken = default,
        Action<FileImportDto>? assertions = null)
    {
        var response = await GetByFileNameAsync(client, fileName, cancellationToken);
        var dto = await ReadDtoAsync(response, cancellationToken);

        dto.Should().NotBeNull();
        dto.FileName.Should().Be(fileName);

        assertions?.Invoke(dto);

        return dto;
    }
}
