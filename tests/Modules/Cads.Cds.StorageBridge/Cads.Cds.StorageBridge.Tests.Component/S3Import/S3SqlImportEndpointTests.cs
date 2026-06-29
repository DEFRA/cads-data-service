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

public class S3SqlImportEndpointTests(StorageBridgeTestFixture testFixture) : IClassFixture<StorageBridgeTestFixture>
{
    private readonly StorageBridgeTestFixture _testFixture = testFixture;

    private const string Endpoint = TestEndpointConstants.StorageBridgeS3SqlBulkLoadRoot;

    [Fact]
    public async Task GivenInvalidRequest_WhenS3ImportRequested_ShouldReturnBadRequest()
    {
        var response = await _testFixture.HttpClient.PostAsync(Endpoint, InvalidS3SqlImportRequest, TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetailsDto>(TestContext.Current.CancellationToken);
        problemDetails.Should().NotBeNull();
        problemDetails.Errors.Should().NotBeNull().And.HaveCount(1);
        problemDetails.Errors["SourceKey"].Should().Contain("'Source Key' must not be empty.");
    }

    [Fact]
    public async Task GivenValidRequest_WhenS3ImportRequested_ShouldSucceed()
    {
        SetupS3MockForSqlFile(TestDataFileConstants.LocationsSqlInsertStatement);

        var response = await _testFixture.HttpClient.PostAsync(Endpoint, ValidS3SqlImportRequest, TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        var job = await _testFixture.Factory.TestSqlImportJobChannel.WaitForJobAsync(TestContext.Current.CancellationToken);
        job.SourceKey.Should().Be("test.sql");
    }

    private static StringContent? InvalidS3SqlImportRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3SqlImportRequest
        {
            SourceKey = string.Empty,
        });

    private static StringContent? ValidS3SqlImportRequest =>
        HttpContentUtility.CreateApplicationJsonAsStringContent(new S3SqlImportRequest
        {
            SourceKey = "test.sql",
        });

    private void SetupS3MockForSqlFile(string row1)
    {
        _testFixture.Factory.AmazonS3Mock.Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() =>
            {
                var fileData = $"{row1}";
                return TestDataFileConstants.FakeFileContent(fileData);
            });
    }
}