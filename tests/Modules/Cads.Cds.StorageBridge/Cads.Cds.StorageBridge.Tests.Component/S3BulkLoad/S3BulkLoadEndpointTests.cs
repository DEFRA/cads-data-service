using Cads.Cds.BuildingBlocks.Testing.Support.ProblemDetails;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Http;
using Cads.Cds.StorageBridge.Controllers.Requests;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using Cads.Cds.StorageBridge.Tests.Component.TestFixtures;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Cads.Cds.StorageBridge.Tests.Component.S3BulkLoad;

public class S3BulkLoadEndpointTests(StorageBridgeTestFixture testFixture) : IClassFixture<StorageBridgeTestFixture>
{
    private readonly StorageBridgeTestFixture _testFixture = testFixture;

    [Fact]
    public async Task GivenInvalidRequest_WhenS3BulkLoadRequested_ShouldReturnBadRequest()
    {
        var endpoint = TestEndpointConstants.StorageBridgeS3BulkLoadRoot;
        var request = new S3BulkLoadRequest 
        {
            SourceKey = string.Empty,
            BulkImportType = BulkLoadDataTypes.None,
            ActionType = ImportActions.None
        };
        var content = HttpContentUtility.CreateApplicationJsonAsStringContent(request);

        var response = await _testFixture.HttpClient.PostAsync(endpoint, content, TestContext.Current.CancellationToken);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetailsDto>(TestContext.Current.CancellationToken);
        problemDetails.Should().NotBeNull();
        problemDetails.Errors.Should().NotBeNull().And.HaveCount(3);
        problemDetails.Errors["SourceKey"].Should().Contain("'Source Key' must not be empty.");
        problemDetails.Errors["BulkImportType"].Should().Contain("'Bulk Import Type' must not be equal to 'None'.");
        problemDetails.Errors["ActionType"].Should().Contain("'Action Type' must not be equal to 'None'.");
    }
}
