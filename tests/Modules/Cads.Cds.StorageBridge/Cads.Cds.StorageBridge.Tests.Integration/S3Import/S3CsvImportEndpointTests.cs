using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Logging;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;
using Cads.Cds.StorageBridge.Controllers.Requests;
using Cads.Cds.StorageBridge.Controllers.Responses;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;
using Cads.Cds.StorageBridge.Testing.Support.BulkLoad.Utilities;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using Cads.Cds.ApiSurface.Dtos.Imports;

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

    private readonly PostgresDb _postgresDb;

    public S3CsvImportEndpointTests(ApiContainerFixture apiContainerFixture)
    {
        this._apiContainerFixture = apiContainerFixture;

        _testFileName = Path.GetFileNameWithoutExtension("CTSM_CADS_PROD_BULK_ABC_0001_CT_LOCATIONS_2026-01-01-012345.csv");
        _testKey = $"import/{Path.GetFileNameWithoutExtension(_testFileName)}/{_testFileName}";

        _postgresDb = new PostgresDb(apiContainerFixture.PostgresFixture.HostConnectionString);

        var insertQuery = @"INSERT INTO cads.cts_file_imports(
	        destination_table_name
	        , file_name
	        , total_rows_to_process
	        , added_at
	        , import_status_id
	        , processing_status_id
	        , rows_found
	        , import_start_at
	        , import_end_at)
	        VALUES
                ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0001_CT_LOCATIONS_2026-01-01-012345', 100, NOW(), 3, 1, 0, NULL, NULL),
                ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0002_CT_LOCATIONS_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL),
		        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0003_CT_LOCATIONS_2026-01-01-012345', 100, NOW(), 3, 1, 0, NOW(), NULL),
                ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0004_CT_LOCATIONS_2026-01-01-012345', 100, NOW(), 4, 1, 0, NOW(), NOW()),
                ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0005_CT_LOCATIONS_2026-01-01-012345', 100, NOW(), 5, 1, 0, NOW(), NOW()),
		        ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0007_CT_LOCATIONS_2026-01-01-012345', 100, NOW(), 1, 1, 0, NULL, NULL),
                ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0008_CT_LOCATIONS_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL),
                ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0009_CT_LOCATIONS_2026-01-01-012345', 100, NOW(), 3, 1, 0, NOW(), NULL),
                ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0010_CT_LOCATIONS_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL),
                ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0011_CT_LOCATIONS_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL),
                ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0012_CT_LOCATIONS_2026-01-01-012345', 100, NOW(), 1, 1, 0, NULL, NULL),
                ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0013_CT_LOCATIONS_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL),
                ('dtn', 'CTSM_CADS_PROD_BULK_ABC_0014_CT_LOCATIONS_2026-01-01-012345', 100, NOW(), 2, 1, 0, NOW(), NULL)
        ON CONFLICT DO NOTHING;";

        _postgresDb.ExecuteNonQueryAsync(insertQuery).ConfigureAwait(false);
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

    private StringContent? ValidS3CsvImportWithFileImportIdRequest =>
       HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvImportRequest
       {
           FileImportId = 3
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

    private async Task<IEnumerable<FileImport>> GetFileImports()
    {
        return await _postgresDb.ExecuteQueryAsync(
           "SELECT * FROM cads.cts_file_imports",
           reader => new FileImport
           {
               Id = reader["cts_file_import_id"] != DBNull.Value ? Convert.ToInt64(reader["cts_file_import_id"]) : 0,
               DestinationTableName = reader["destination_table_name"].ToString()!,
               FileName = reader["file_name"].ToString()!,
               TotalRowsToProcess = reader["total_rows_to_process"] != DBNull.Value ? Convert.ToInt64(reader["total_rows_to_process"]) : 0,
               AddedAt = reader["added_at"] != DBNull.Value ? Convert.ToDateTime(reader["added_at"]) : DateTime.MinValue,
               ImportStatus = reader["import_status_id"] != DBNull.Value ? (FileImportStatus)Convert.ToInt32(reader["import_status_id"]) : FileImportStatus.Pending,
               ProcessingStatus = reader["processing_status_id"] != DBNull.Value ? (FileProcessingStatus)Convert.ToInt32(reader["processing_status_id"]) : FileProcessingStatus.Pending,
               RowsFound = reader["rows_found"] != DBNull.Value ? Convert.ToInt64(reader["rows_found"]) : 0,
               ImportStartAt = reader["import_start_at"] != DBNull.Value ? Convert.ToDateTime(reader["import_start_at"]) : null,
               ImportEndAt = reader["import_end_at"] != DBNull.Value ? Convert.ToDateTime(reader["import_end_at"]) : null
           });
    }
}