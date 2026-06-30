using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Testing.Support.ProblemDetails;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.StorageBridge.Controllers.Requests;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
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

    [Fact]
    public async Task GivenInvalidRequest_WhenS3CsvImportRequested_ShouldReturnBadRequest()
    {
        var response = await _testFixture.HttpClient.PostAsync(Endpoint, InvalidS3ImportRequest, TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetailsDto>(TestContext.Current.CancellationToken);
        problemDetails.Should().NotBeNull();
        problemDetails.Errors.Should().NotBeNull().And.HaveCount(3);
        problemDetails.Errors["SourceKey"].Should().Contain("'Source Key' must not be empty.");
        problemDetails.Errors["ImportDataType"].Should().Contain("'Import Data Type' must not be equal to 'None'.");
        problemDetails.Errors["ImportActionType"].Should().Contain("'Import Action Type' must not be equal to 'None'.");
    }

    [Fact]
    public async Task GivenValidRequest_WhenS3ImportRequested_ShouldSucceed()
    {
        SetupS3MockForLocations(TestDataFileConstants.LocationsDataRow1, TestDataFileConstants.LocationsDataRow2);

        var response = await _testFixture.HttpClient.PostAsync(Endpoint, ValidS3ImportRequest, TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await _testFixture.Factory.TestCsvBulkLoadJobChannel.WaitForJobAsync(TestContext.Current.CancellationToken);
        job.SourceKey.Should().Be("LOCATIONS.part-0001.csv");
        job.ImportDataType.Should().Be(ImportDataType.CtLocations);
    }

    private static StringContent? InvalidS3ImportRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvImportRequest
        {
            SourceKey = string.Empty,
            ImportDataType = ImportDataType.None,
            ImportActionType = ImportActionType.None
        });

    private static StringContent? ValidS3ImportRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvImportRequest
        {
            SourceKey = "LOCATIONS.part-0001.csv",
            ImportDataType = ImportDataType.CtLocations,
            ImportActionType = ImportActionType.Bulk
        });

    private static StringContent? ValidS3TransactionalRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3CsvImportRequest
        {
            SourceKey = "LOCATIONS.part-0001.csv",
            ImportDataType = ImportDataType.CtLocations,
            ImportActionType = ImportActionType.Transactional
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