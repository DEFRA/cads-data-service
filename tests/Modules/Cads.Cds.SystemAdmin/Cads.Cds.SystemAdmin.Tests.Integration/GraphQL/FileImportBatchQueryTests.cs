using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Cads.Cds.SystemAdmin.Tests.Integration.GraphQL;

[Collection("SystemAdminIntegration"), Trait("Dependence", "testcontainers")]
public class FileImportBatchQueryTests(ApiContainerFixture apiContainerFixture)
{
    private const string Endpoint = "/graphql/cads";

    private const string FileImportBatchQuery = @"query ($groupKey: String!) {
        fileImportBatch(groupKey: $groupKey) {
            groupKey
            importComplete
            processingComplete
            parts {
                partNo
                fileName
                importStatusId
                processingStatusId
                totalRowsToProcess
                rowsFound
                rowsImported
                failedAttempts
                lastErrorReason
                lastFilePartImported
            }
        }
    }";

    private const string ExistingCadsQuery = "{ erd { entity } }";

    private HttpClient _httpClient => apiContainerFixture.CreateBasicClient();

    private readonly PostgresDb _postgresDb = new(apiContainerFixture.PostgresFixture.HostConnectionString);

    public static TheoryData<string> InvalidCredentials => new() { "none", "invalid-api-key" };

    [Theory]
    [MemberData(nameof(InvalidCredentials))]
    public async Task GivenInvalidCredentials_WhenFileImportBatchRequested_ShouldBeRejectedLikeExistingCadsQuery(string credentials)
    {
        var fileImportBatchResponse = await PostAsync(
            CreateClient(credentials),
            FileImportBatchQuery,
            new { groupKey = "any" });

        var existingQueryResponse = await PostAsync(
            CreateClient(credentials),
            ExistingCadsQuery);

        fileImportBatchResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        fileImportBatchResponse.StatusCode.Should().Be(existingQueryResponse.StatusCode);
    }

    [Fact]
    public async Task GivenUnknownGroupKey_WhenFileImportBatchRequested_ShouldReturnNull()
    {
        var response = await PostAsync(
            _httpClient,
            FileImportBatchQuery,
            new { groupKey = $"CTSM_CADS_TEST_BULK_{Guid.NewGuid():N}_CT_LOCATIONS" });

        response.IsSuccessStatusCode.Should().BeTrue();

        var body = await ReadBodyAsync(response);

        body["errors"].Should().BeNull();
        body["data"]!.AsObject().ContainsKey("fileImportBatch").Should().BeTrue();
        body["data"]!["fileImportBatch"].Should().BeNull();
    }

    [Fact]
    public async Task GivenExistingGroupKey_WhenFileImportBatchRequested_ShouldReturnBatch()
    {
        var batchId = Guid.NewGuid().ToString("N");
        var groupKey = $"CTSM_CADS_TEST_BULK_{batchId}_CT_LOCATIONS";
        var part1FileName = $"CTSM_CADS_TEST_BULK_{batchId}_1_CT_LOCATIONS_2026-11-04-120000.csv";
        var part2FileName = $"CTSM_CADS_TEST_BULK_{batchId}_2_CT_LOCATIONS_2026-11-04-120000.csv";

        try
        {
            await _postgresDb.InsertFileImportAsync(part2FileName, groupKey, FileImportStatus.Completed);
            await _postgresDb.InsertFileImportAsync(part1FileName, groupKey, FileImportStatus.Completed);

            var response = await PostAsync(
                _httpClient,
                FileImportBatchQuery,
                new { groupKey });

            response.IsSuccessStatusCode.Should().BeTrue();

            var body = await ReadBodyAsync(response);

            body["errors"].Should().BeNull();

            var batch = body["data"]!["fileImportBatch"]!;

            batch["groupKey"]!.GetValue<string>().Should().Be(groupKey);
            batch["importComplete"]!.GetValue<bool>().Should().BeTrue();
            batch["processingComplete"]!.GetValue<bool>().Should().BeFalse();

            var parts = batch["parts"]!.AsArray();

            parts.Should().HaveCount(2);

            parts[0]!["partNo"]!.GetValue<int>().Should().Be(1);
            parts[0]!["fileName"]!.GetValue<string>().Should().Be(part1FileName);
            parts[1]!["partNo"]!.GetValue<int>().Should().Be(2);
            parts[1]!["fileName"]!.GetValue<string>().Should().Be(part2FileName);

            foreach (var part in parts)
            {
                part!["importStatusId"]!.GetValue<short>().Should().Be((short)FileImportStatus.Completed);
                part["processingStatusId"]!.GetValue<short>().Should().Be((short)FileProcessingStatus.Pending);
                part["totalRowsToProcess"]!.GetValue<long>().Should().Be(1);
                part["rowsFound"]!.GetValue<long>().Should().Be(1);
                part["rowsImported"]!.GetValue<long>().Should().Be(0);
                part["failedAttempts"]!.GetValue<short>().Should().Be(0);
                part["lastErrorReason"].Should().BeNull();
                part["lastFilePartImported"].Should().BeNull();
            }
        }
        finally
        {
            await _postgresDb.DeleteFileImportByGroupKeyAsync(groupKey);
        }
    }

    [Fact]
    public async Task GivenQueryDeeperThanFourLevels_WhenRequested_ShouldBeRejected()
    {
        var response = await PostAsync(
            _httpClient,
            "{ ctsFileImports { nodes { importStatus { ctsFileImports { fileName } } } } }");

        var content = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        var body = JsonNode.Parse(content)!;

        body["errors"].Should().NotBeNull(content);
        body["errors"]![0]!["message"]!.GetValue<string>().Should().ContainEquivalentOf("depth", content);
        body["data"].Should().BeNull(content);
    }

    private HttpClient CreateClient(string credentials)
    {
        var client = new HttpClient { BaseAddress = apiContainerFixture.CreateClient().BaseAddress };

        if (credentials == "invalid-api-key")
        {
            client.AddBasicApiKey("invalid-key", "invalid-secret");
        }

        return client;
    }

    private static Task<HttpResponseMessage> PostAsync(HttpClient client, string query, object? variables = null)
    {
        return client.PostAsJsonAsync(
            Endpoint,
            new { query, variables },
            TestContext.Current.CancellationToken);
    }

    private static async Task<JsonNode> ReadBodyAsync(HttpResponseMessage response)
    {
        return JsonNode.Parse(await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken))!;
    }
}