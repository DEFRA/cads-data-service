using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Application.Extensions;
using Cads.Cds.BuildingBlocks.Application.Imports.Attributes;
using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Logging;
using Cads.Cds.StorageBridge.Controllers.Requests;
using Cads.Cds.StorageBridge.Controllers.Responses;
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
    public async Task GivenInvalidRequest_WhenS3SqlImportRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest(InvalidS3SqlImportRequest);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenNoDataRowsExist_WhenS3SqlImportRequested_ShouldCreateNoRecords()
    {
        var fileData = " ";

        var sourceKey = "no-data-rows-exist.sql";
        await apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = sourceKey,
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3SqlImportRequest(sourceKey));

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        await response.Content.ReadFromJsonAsync<JobResponse>(TestContext.Current.CancellationToken);

        await VerifyLoggingMessage($"SQL script file \"{sourceKey}\" is empty — skipping.");
    }

    [Fact]
    public async Task GivenInvalidDataRowsExist_WhenS3ImportRequested_ShouldFail()
    {
        var fileData = TestDataFileConstants.InvalidLocationsSqlInsertStatement;

        var sourceKey = "invalid-data-rows-exist.sql";
        await apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = sourceKey,
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3SqlImportRequest(sourceKey));

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        await response.Content.ReadFromJsonAsync<JobResponse>(TestContext.Current.CancellationToken);

        await VerifyLoggingMessage($"Failed to execute SQL script file \"{sourceKey}\"");
    }

    [Fact]
    public async Task GivenValidRequest_WhenS3ImportRequested_ShouldSucceed()
    {
        var fileData = $"{TestDataFileConstants.LocationsSqlInsertStatement}";

        var sourceKey = "valid-data-rows-exist.sql";
        await apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = sourceKey,
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3SqlImportRequest(sourceKey));

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

        await VerifyLoggingMessage($"Completed SQL script execution for prefix \"{sourceKey}\".");
    }

    private static StringContent? InvalidS3SqlImportRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3SqlImportRequest
        {
            SourceKey = string.Empty
        });

    private static StringContent? ValidS3SqlImportRequest(string sourceKey) =>
       HttpContentUtility.CreateApplicationJsonAsStringContent(new S3SqlImportRequest
       {
           SourceKey = sourceKey
       });

    private async Task<HttpResponseMessage> ExecuteTest(StringContent? payload)
    {
        var endpoint = TestEndpointConstants.StorageBridgeS3SqlImportRoot;
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