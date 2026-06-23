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

namespace Cads.Cds.StorageBridge.Tests.Integration.S3BulkLoad;

[Collection("StorageBridgeIntegration"), Trait("Dependence", "testcontainers")]
public class S3CsvBulkLoadEndpointTests(ApiContainerFixture apiContainerFixture)
{
    private const int ProcessingTimeCircuitBreakerSeconds = 30;

    [Fact]
    public async Task GivenInvalidRequest_WhenS3BulkLoadRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest(InvalidS3CsvBulkLoadRequest);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenHeadingRowMissing_WhenS3BulkLoadRequested_ShouldFail()
    {
        var fileData = $"{TestDataFileConstants.LocationsDataRow1}\n" +
                       $"{TestDataFileConstants.LocationsDataRow2}";

        await apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = "LOCATIONS.part-0001.csv",
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3CsvBulkLoadRequest);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        await VerifyLoggingMessage("File LOCATIONS.part-0001.csv does not contain a valid header row.");
    }

    [Fact]
    public async Task GivenNoDataRowsExist_WhenS3BulkLoadRequested_ShouldCreateNoRecords()
    {
        var fileData = $"{TestDataFileConstants.LocationsHeader}";

        await apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = "LOCATIONS.part-0001.csv",
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3CsvBulkLoadRequest);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await response.Content.ReadFromJsonAsync<JobResponse>(TestContext.Current.CancellationToken);

        await VerifyLoggingMessage($"Completed bulk import copy for job {job!.JobId} with key sourceKey \"LOCATIONS.part-0001.csv\", 0 records processed");
    }

    [Fact]
    public async Task GivenInvalidDataRowsExist_WhenS3BulkLoadRequested_ShouldFail()
    {
        var fileData = $"{TestDataFileConstants.LocationsHeader}\n" +
                       $"{TestDataFileConstants.LocationsDataRow1}\n" +
                       $"{TestDataFileConstants.InvalidLocationsDataRow1}";

        await apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = "LOCATIONS.part-0001.csv",
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3CsvBulkLoadRequest);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await response.Content.ReadFromJsonAsync<JobResponse>(TestContext.Current.CancellationToken);

        await VerifyLoggingMessage($"Failed to process bulk load job {job!.JobId}");
    }

    [Fact]
    public async Task GivenValidRequest_WhenS3BulkLoadRequested_ShouldSucceed()
    {
        var fileData = $"{TestDataFileConstants.LocationsHeader}\n" +
                       $"{TestDataFileConstants.LocationsDataRow1}\n" +
                       $"{TestDataFileConstants.LocationsDataRow2}";

        await apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = "LOCATIONS.part-0001.csv",
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3CsvBulkLoadRequest);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await response.Content.ReadFromJsonAsync<JobResponse>(TestContext.Current.CancellationToken);

        var attribute = BulkLoadDataType.Locations.GetAttribute<TableNameAttribute>()!;
        var schemaName = attribute.Schema.GetDescription();
        var tableName = string.IsNullOrWhiteSpace(schemaName)
            ? attribute.Name
            : $"{schemaName}.{attribute.Name}";

        await BulkLoadTestHelpers.AssertCsvRowsMatchDatabaseAsync(
            apiContainerFixture.PostgresFixture.HostConnectionString,
            $"SELECT * FROM {tableName} WHERE loc_id >= 101 AND loc_id <= 102 ORDER BY loc_id",
            [
                TestDataFileConstants.LocationsDataRow1,
                TestDataFileConstants.LocationsDataRow2
            ]);

        await VerifyLoggingMessage($"Completed bulk import copy for job {job!.JobId} with key sourceKey \"LOCATIONS.part-0001.csv\", 2 records processed");
    }

    private static StringContent? InvalidS3CsvBulkLoadRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvBulkLoadRequest
        {
            SourceKey = string.Empty,
            BulkImportType = BulkLoadDataType.None,
            ActionType = ImportActions.None
        });

    private static StringContent? ValidS3CsvBulkLoadRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvBulkLoadRequest
        {
            SourceKey = "LOCATIONS.part-0001.csv",
            BulkImportType = BulkLoadDataType.Locations,
            ActionType = ImportActions.Insert
        });

    private async Task<HttpResponseMessage> ExecuteTest(StringContent? payload)
    {
        var endpoint = TestEndpointConstants.StorageBridgeS3CsvBulkLoadRoot;
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