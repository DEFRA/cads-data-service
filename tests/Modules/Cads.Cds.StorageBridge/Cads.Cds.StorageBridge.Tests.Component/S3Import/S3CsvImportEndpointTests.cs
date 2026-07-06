using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Testing.Support.ProblemDetails;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.StorageBridge.Controllers.Requests;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using Cads.Cds.StorageBridge.Tests.Component.TestFixtures;
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
        problemDetails.Errors["SourceKey"].Should().Contain("'Source Key' must not be empty.");
    }

    [Fact]
    public async Task GivenValidRequest_WhenS3BulkImportRequested_ShouldSucceed()
    {
        SetupS3MockForLocations(TestDataFileConstants.LocationsDataRow1, TestDataFileConstants.LocationsDataRow2);

        var response = await _testFixture.HttpClient.PostAsync(Endpoint, ValidS3BulkImportRequest, TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await _testFixture.Factory.TestCsvBulkLoadJobChannel.WaitForJobAsync(TestContext.Current.CancellationToken);
        job.SourceKey.Should().Be(ValidBulkSourceKey);
    }


    [Fact]
    public async Task GivenValidRequest_WhenS3DeltaImportRequested_ShouldSucceed()
    {
        SetupS3MockForLocations(TestDataFileConstants.LocationsDataRow1, TestDataFileConstants.LocationsDataRow2);

        var response = await _testFixture.HttpClient.PostAsync(Endpoint, ValidS3DeltaRequest, TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await _testFixture.Factory.TestCsvBulkLoadJobChannel.WaitForJobAsync(TestContext.Current.CancellationToken);
        job.SourceKey.Should().Be(ValidDeltaSourceKey);
    }

    private static StringContent? InvalidS3ImportRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvImportRequest
        {
            SourceKey = string.Empty
        });

    private static StringContent? ValidS3BulkImportRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvImportRequest
        {
            SourceKey = ValidBulkSourceKey
        });

    private static StringContent? ValidS3DeltaRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvImportRequest
        {
            SourceKey = ValidDeltaSourceKey
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