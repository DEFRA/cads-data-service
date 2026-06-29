using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Logging;
using Cads.Cds.StorageBridge.Controllers.Requests;
using Cads.Cds.StorageBridge.Controllers.Responses;
using Cads.Cds.StorageBridge.Core.Attributes;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Testing.Support.BulkLoad.Utilities;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Cads.Cds.StorageBridge.Tests.Integration.S3Import;

[Collection("StorageBridgeIntegration"), Trait("Dependence", "testcontainers")]
public class S3SqlImportEndpointTests(ApiContainerFixture apiContainerFixture)
{
    private const int ProcessingTimeCircuitBreakerSeconds = 30;

    [Fact]
    public async Task GivenInvalidRequest_WhenS3BulkLoadRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest(InvalidS3SqlBulkLoadRequest);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenNoDataRowsExist_WhenS3BulkLoadRequested_ShouldCreateNoRecords()
    {
        var fileData = " ";

        await apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = "test.sql",
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3SqlBulkLoadRequest);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await response.Content.ReadFromJsonAsync<JobResponse>(TestContext.Current.CancellationToken);

        await VerifyLoggingMessage($"SQL script file \"test.sql\" is empty — skipping.");
    }

    [Fact]
    public async Task GivenInvalidDataRowsExist_WhenS3BulkLoadRequested_ShouldFail()
    {
        var fileData = TestDataFileConstants.InvalidLocationsSqlInsertStatement;

        await apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = "test.sql",
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3SqlBulkLoadRequest);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await response.Content.ReadFromJsonAsync<JobResponse>(TestContext.Current.CancellationToken);

        await VerifyLoggingMessage($"Failed to execute SQL script file \"test.sql\"");
    }

    [Fact]
    public async Task GivenValidRequest_WhenS3BulkLoadRequested_ShouldSucceed()
    {
        var fileData = $"{TestDataFileConstants.LocationsSqlInsertStatement}";

        await apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = "test.sql",
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3SqlBulkLoadRequest);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await response.Content.ReadFromJsonAsync<JobResponse>(TestContext.Current.CancellationToken);

        var attributes = ImportDataType.CtLocations.GetAttributes<TableInfoAttribute>()!;
        var schemaName = attributes.First().Schema.GetDescription();
        var tableName = string.IsNullOrWhiteSpace(schemaName)
            ? attributes.First().Name
            : $"{schemaName}.{attributes.First().Name}";

        await BulkLoadTestHelpers.AssertRowsMatchDatabaseAsync(
            apiContainerFixture.PostgresFixture.HostConnectionString,
            $"SELECT * FROM {tableName} WHERE loc_id = {TestDataFileConstants.LocationsSqlInsertDataDictionary["loc_id"]}",
            [LocationRecordUtilities.MapLocation(TestDataFileConstants.LocationsSqlInsertDataDictionary)],
            LocationRecordUtilities.MapLocation);

        await VerifyLoggingMessage($"Completed SQL script execution for prefix \"test.sql\".");
    }

    private static StringContent? InvalidS3SqlBulkLoadRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3SqlImportRequest
        {
            SourceKey = string.Empty
        });

    private static StringContent? ValidS3SqlBulkLoadRequest =>
       HttpContentUtility.CreateApplicationJsonAsStringContent(new S3SqlImportRequest
       {
           SourceKey = "test.sql"
       });

    private async Task<HttpResponseMessage> ExecuteTest(StringContent? payload)
    {
        var endpoint = TestEndpointConstants.StorageBridgeS3SqlBulkLoadRoot;
        var client = apiContainerFixture.CreateBasicClient();

        return await client.PostAsync(endpoint, payload, TestContext.Current.CancellationToken);
    }

    private async Task VerifyLoggingMessage(string message)
    {
        var timeout = TimeSpan.FromSeconds(ProcessingTimeCircuitBreakerSeconds);
        var pollInterval = TimeSpan.FromSeconds(2);

        var startTime = DateTime.UtcNow;
        var foundLogEntry = false;

        while (DateTime.UtcNow - startTime < timeout)
        {
            foundLogEntry = await ContainerLoggingUtility.FindContainerLogEntryAsync(
                apiContainerFixture.ApiContainer,
                message);

            if (foundLogEntry)
                break;

            await Task.Delay(pollInterval);
        }

        foundLogEntry.Should().BeTrue($"Expected log entry within {ProcessingTimeCircuitBreakerSeconds} seconds but none was found.");
    }
}