using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Authorization;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.CircuitBreakers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.StorageBridge.Application.Extensions;
using Cads.Cds.StorageBridge.Controllers.Requests;
using Cads.Cds.StorageBridge.Core.Attributes;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using FluentAssertions;
using Npgsql;
using System.Net;

namespace Cads.Cds.StorageBridge.Tests.Integration.S3BulkLoad;

[Collection("StorageBridgeIntegration"), Trait("Dependence", "testcontainers")]
public class S3BulkLoadEndpointTests(ApiContainerFixture apiContainerFixture)
{
    [Fact]
    public async Task GivenInvalidRequest_WhenS3BulkLoadRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest(InvalidS3BulkLoadRequest);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenValidRequest_WhenS3BulkLoadRequested_ShouldSucceed()
    {
        var fileData = $"{TestDataFileConstants.LocationsHeader}\n{TestDataFileConstants.LocationsDataRow1}\n{TestDataFileConstants.LocationsDataRow2}";
        await apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = "LOCATIONS.part-0001.csv",
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3BulkLoadRequest);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var responseBody = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        responseBody.Should().NotBeNull().And.Contain("jobId");

        var tableName = BulkLoadDataTypes.Locations.GetAttribute<TableNameAttribute>()!.Name;
        var count = await CircuitBreakerUtilities.WaitUntilAsync(
            action: async () =>
            {
                await using var conn = new NpgsqlConnection(apiContainerFixture.PostgresFixture.HostConnectionString);
                await conn.OpenAsync();
                await using var cmd = new NpgsqlCommand($"SELECT COUNT(1) FROM {tableName}", conn);
                return (long?)await cmd.ExecuteScalarAsync();
            },
            condition: c => c == 1,
            timeout: TimeSpan.FromSeconds(10),
            pollInterval: TimeSpan.FromMilliseconds(200));

        count.Should().Be(2);
    }

    private static StringContent? InvalidS3BulkLoadRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3BulkLoadRequest
        {
            SourceKey = string.Empty,
            BulkImportType = BulkLoadDataTypes.None,
            ActionType = ImportActions.None
        });

    private static StringContent? ValidS3BulkLoadRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3BulkLoadRequest
        {
            SourceKey = "LOCATIONS.part-0001.csv",
            BulkImportType = BulkLoadDataTypes.Locations,
            ActionType = ImportActions.Insert
        });

    private async Task<HttpResponseMessage> ExecuteTest(StringContent? payload)
    {
        var endpoint = TestEndpointConstants.StorageBridgeS3BulkLoadRoot;
        var client = await apiContainerFixture.CreateAzureAdClientAsync(TestTokenFactory.ValidUserToken());

        return await client.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
    }
}
