using Amazon.S3.Model;
using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Logging;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;
using Cads.Cds.StorageBridge.Controllers.Requests;
using Cads.Cds.StorageBridge.Controllers.Responses;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;
using Cads.Cds.StorageBridge.Testing.Support.BulkLoad.Utilities;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Cads.Cds.StorageBridge.Tests.Integration.S3Import;

[Collection("StorageBridgeIntegration"), Trait("Dependence", "testcontainers")]
public class S3CsvImportEndpointTests
{
    private const int ProcessingTimeCircuitBreakerSeconds = 30;

    private readonly ApiContainerFixture _apiContainerFixture;

    private readonly string _testFileName;
    private readonly string _testKey;

    private readonly string _testGroupKey;

    private readonly PostgresDb _postgresDb;

    public S3CsvImportEndpointTests(ApiContainerFixture apiContainerFixture)
    {
        _apiContainerFixture = apiContainerFixture;

        _testFileName = Path.GetFileNameWithoutExtension("CTSM_CADS_PROD_BULK_ABC_0001_CT_LOCATIONS_2026-01-01-012345.csv");
        _testKey = $"import/{Path.GetFileNameWithoutExtension(_testFileName)}/{_testFileName}";
        _testGroupKey = "CTSM_CADS_PROD_BULK_ABC_CT_LOCATIONS";

        _postgresDb = new PostgresDb(apiContainerFixture.PostgresFixture.HostConnectionString);

        _postgresDb.InsertFileImportAsync(_testFileName, _testGroupKey, FileImportStatus.Split).ConfigureAwait(false);
    }

    [Fact]
    public async Task GivenInvalidRequest_WhenS3CsvImportRequested_ShouldReturnBadRequest()
    {
        var response = await ExecuteTest(InvalidS3CsvImportRequest);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenHeadingRowMissing_WhenS3CsvImportRequested_ShouldFail()
    {
        var fileData = $"{TestDataFileConstants.LocationsDataRow1}\n" +
                       $"{TestDataFileConstants.LocationsDataRow2}";

        await _apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = _testKey,
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3CsvImportWithSourceKeyRequest);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        await VerifyLoggingMessage($"File {_testKey} does not contain a valid header row.");
    }

    [Fact]
    public async Task GivenNoDataRowsExist_WhenS3CsvImportRequested_ShouldCreateNoRecords()
    {
        var fileData = $"{TestDataFileConstants.LocationsHeader}";

        await _apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = _testKey,
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3CsvImportWithSourceKeyRequest);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await response.Content.ReadFromJsonAsync<JobResponse>(TestContext.Current.CancellationToken);

        await VerifyLoggingMessage($"Completed CSV import copy for job {job!.JobId} with key \"{_testFileName}\", 0 records processed");
    }

    [Fact]
    public async Task GivenInvalidDataRowsExist_WhenS3CsvImportRequested_ShouldFail()
    {
        var fileData = $"{TestDataFileConstants.LocationsHeader}\n" +
                       $"{TestDataFileConstants.LocationsDataRow1}\n" +
                       $"{TestDataFileConstants.InvalidLocationsDataRow1}";

        await _apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = _testKey,
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3CsvImportWithSourceKeyRequest);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await response.Content.ReadFromJsonAsync<JobResponse>(TestContext.Current.CancellationToken);

        await VerifyLoggingMessage($"Failed to process bulk load job {job!.JobId}");
    }

    [Fact]
    public async Task GivenValidRequest_WhenS3CsvImportRequested_ShouldSucceed()
    {
        var fileData = $"{TestDataFileConstants.LocationsHeader}\n" +
                       $"{TestDataFileConstants.LocationsDataRow1}\n" +
                       $"{TestDataFileConstants.LocationsDataRow2}";

        await _apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = _testKey,
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);

        var response = await ExecuteTest(ValidS3CsvImportWithSourceKeyRequest);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await response.Content.ReadFromJsonAsync<JobResponse>(TestContext.Current.CancellationToken);

        var tableName = S3ImportCommandFactory.GetTableName(ImportDataType.CtLocations, SchemaName.CtsTransactions);

        await BulkLoadTestHelpers.AssertCsvRowsMatchDatabaseAsync(
            _apiContainerFixture.PostgresFixture.HostConnectionString,
            $"SELECT * FROM {tableName} WHERE loc_id >= 101 AND loc_id <= 102 ORDER BY loc_id",
            [
                TestDataFileConstants.LocationsDataRow1,
                TestDataFileConstants.LocationsDataRow2
            ]);

        await VerifyLoggingMessage($"Completed CSV import copy for job {job!.JobId} with key \"{_testFileName}\", 2 records processed");
    }

    private static StringContent? InvalidS3CsvImportRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvImportRequest
        {
            SourceKey = string.Empty
        });

    private StringContent? ValidS3CsvImportWithSourceKeyRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvImportRequest
        {
            SourceKey = _testFileName
        });

    private async Task<HttpResponseMessage> ExecuteTest(StringContent? payload)
    {
        var endpoint = TestEndpointConstants.StorageBridgeS3CsvImportRoot;
        var client = _apiContainerFixture.CreateBasicClient();

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
                _apiContainerFixture.ApiContainer,
                message);

            if (foundLogEntry)
                break;

            await Task.Delay(pollInterval);
        }

        foundLogEntry.Should().BeTrue($"Expected log entry within {ProcessingTimeCircuitBreakerSeconds} seconds but none was found.");
    }
}