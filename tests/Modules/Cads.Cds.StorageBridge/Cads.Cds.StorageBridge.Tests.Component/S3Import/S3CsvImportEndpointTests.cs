using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Testing.Support.ProblemDetails;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.StorageBridge.Controllers.Requests;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using Cads.Cds.StorageBridge.Tests.Component.TestFixtures;
using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using FluentAssertions;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace Cads.Cds.StorageBridge.Tests.Component.S3Import;

public class S3CsvImportEndpointTests(StorageBridgeTestFixture testFixture) : IClassFixture<StorageBridgeTestFixture>
{
    private readonly StorageBridgeTestFixture _testFixture = testFixture;

    private const string Endpoint = TestEndpointConstants.StorageBridgeS3CsvImportRoot;

    // Filename template:CTSM_CADS_<env>_<type>_<batchId>_<partno>_<tablename>_<YYYY-MM-DD-hhmmss>.csv
    private const string ValidBulkSourceKey = "CTSM_CADS_BULK_0001_0001_CT_LOCATIONS.part-0001.csv";
    private const string ValidDeltaSourceKey = "CTSM_CADS_DELTA_0001_0001_CT_LOCATIONS.part-0001.csv";

    [Fact]
    public async Task GivenInvalidRequest_WhenS3CsvImportRequested_ShouldReturnBadRequest()
    {
        var response = await _testFixture.HttpClient.PostAsync(Endpoint, InvalidS3ImportRequest, TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetailsDto>(TestContext.Current.CancellationToken);
        problemDetails.Should().NotBeNull();
        problemDetails.Errors.Should().NotBeNull().And.HaveCount(1);
        problemDetails.Errors["FileImportId"].Should().Contain("'File Import Id' must not be null or greater than zero.");
    }

    [Fact]
    public async Task GivenValidRequest_WhenS3BulkImportWithFileImportIdRequested_ShouldSucceed()
    {
        SetupS3MockForLocations(TestDataFileConstants.LocationsDataRow1, TestDataFileConstants.LocationsDataRow2);

        var response = await _testFixture.HttpClient.PostAsync(Endpoint, ValidS3BulkImportWithFileImportIdRequest, TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await _testFixture.Factory.TestCsvBulkLoadJobChannel.WaitForJobAsync(TestContext.Current.CancellationToken);
        job.FileImportId.Should().Be(1234);
    }

    [Fact]
    public async Task GivenValidRequest_WhenS3BulkImportWithSourceKeyRequested_ShouldSucceed()
    {
        SetupS3MockForLocations(TestDataFileConstants.LocationsDataRow1, TestDataFileConstants.LocationsDataRow2);

        _testFixture.Factory.FileImportRepository.Setup(x => x.GetByFileNameAsync(ValidBulkSourceKey, It.IsAny<CancellationToken>()))
        .ReturnsAsync(() =>
        {
            return new FileImport { Id = 1234 };
        });

        var response = await _testFixture.HttpClient.PostAsync(Endpoint, ValidS3BulkImportWithSourceKeyRequest, TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await _testFixture.Factory.TestCsvBulkLoadJobChannel.WaitForJobAsync(TestContext.Current.CancellationToken);
        job.FileImportId.Should().Be(1234);
    }


    [Fact]
    public async Task GivenValidRequest_WhenS3DeltaImportWithFileImportIdRequested_ShouldSucceed()
    {
        SetupS3MockForLocations(TestDataFileConstants.LocationsDataRow1, TestDataFileConstants.LocationsDataRow2);

        var response = await _testFixture.HttpClient.PostAsync(Endpoint, ValidS3DeltaWithFileImportIdRequest, TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await _testFixture.Factory.TestCsvBulkLoadJobChannel.WaitForJobAsync(TestContext.Current.CancellationToken);
        job.FileImportId.Should().Be(5678);
    }

    [Fact]
    public async Task GivenValidRequest_WhenS3DeltaImportWithSourceKeyRequested_ShouldSucceed()
    {
        SetupS3MockForLocations(TestDataFileConstants.LocationsDataRow1, TestDataFileConstants.LocationsDataRow2);

        _testFixture.Factory.FileImportRepository.Setup(x => x.GetByFileNameAsync(ValidDeltaSourceKey, It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
            {
                return new FileImport { Id = 5678 };
            });

        var response = await _testFixture.HttpClient.PostAsync(Endpoint, ValidS3DeltaWithSourceKeyRequest, TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await _testFixture.Factory.TestCsvBulkLoadJobChannel.WaitForJobAsync(TestContext.Current.CancellationToken);
        job.FileImportId.Should().Be(5678);
    }

    private static StringContent? InvalidS3ImportRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvImportRequest
        {
            FileImportId = 0
        });

    private static StringContent? ValidS3BulkImportWithFileImportIdRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvImportRequest
        {
            FileImportId = 1234
        });

    private static StringContent? ValidS3BulkImportWithSourceKeyRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvImportRequest
        {
            SourceKey = ValidBulkSourceKey,
        });

    private static StringContent? ValidS3DeltaWithFileImportIdRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvImportRequest
        {
            FileImportId = 5678
        });

    private static StringContent? ValidS3DeltaWithSourceKeyRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvImportRequest
        {
            SourceKey = ValidDeltaSourceKey,
        });

    private void SetupS3MockForLocations(string row1, string row2)
    {
        _testFixture.Factory.AmazonS3Mock.Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
            {
                var fileData = $"{TestDataFileConstants.LocationsHeader}\n{row1}\n{row2}";
                return TestDataFileConstants.FakeFileContent(fileData);
            });
    }
}