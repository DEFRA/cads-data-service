using Cads.Cds.SystemAdmin.Testing.Support.Constants;
using Cads.Cds.SystemAdmin.Tests.Component.TestFixtures;
using FluentAssertions;
using System.Net;

namespace Cads.Cds.SystemAdmin.Tests.Component.Endpoints;

public class FileImportEndpointTests(SystemAdminTestFixture testFixture) : IClassFixture<SystemAdminTestFixture>
{
    private readonly SystemAdminTestFixture _testFixture = testFixture;

    // FileImports - GetByFileName

    [Fact]
    public async Task GivenInvalidRequest_WhenGetByFileNameRequested_ShouldReturnBadRequest()
    {
        var endpoint = string.Format(TestEndpointConstants.FileImportsGetByFileNameEndpoint, " ");

        var response = await _testFixture.HttpClient.GetAsync(endpoint, TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownFileName_WhenGetByFileNameRequested_ShouldReturnNotFound()
    {

    }

    [Fact]
    public async Task GivenValidFileName_WhenGetByFileNameRequested_ShouldSucceed()
    {

    }

    // FileImports - Create

    [Fact]
    public async Task GivenInvalidRequest_WhenCreateRequested_ShouldReturnBadRequest()
    {

    }

    [Fact]
    public async Task GivenValidRequest_WhenCreateRequested_ShouldSucceed()
    {

    }

    // FileImports - MarkImporting

    [Fact]
    public async Task GivenInvalidRequest_WhenMarkImportingRequested_ShouldReturnBadRequest()
    {

    }

    [Fact]
    public async Task GivenUnknownRecord_WhenMarkImportingRequested_ShouldReturnNotFound()
    {

    }

    [Fact]
    public async Task GivenRecordHasInvalidState_WhenMarkImportingRequested_ShouldReturnConflict()
    {

    }

    [Fact]
    public async Task GivenValidRequest_WhenMarkImportingRequested_ShouldSucceed()
    {

    }

    // FileImports - MarkImportComplete

    [Fact]
    public async Task GivenInvalidRequest_WhenMarkImportCompleteRequested_ShouldReturnBadRequest()
    {

    }

    [Fact]
    public async Task GivenUnknownRecord_WhenMarkImportCompleteRequested_ShouldReturnNotFound()
    {

    }

    [Fact]
    public async Task GivenRecordHasInvalidState_WhenMarkImportCompleteRequested_ShouldReturnConflict()
    {

    }

    [Fact]
    public async Task GivenValidRequest_WhenMarkImportCompleteRequested_ShouldSucceed()
    {

    }

    // FileImports - MarkImportFailed

    [Fact]
    public async Task GivenInvalidRequest_WhenMarkImportFailedRequested_ShouldReturnBadRequest()
    {

    }

    [Fact]
    public async Task GivenUnknownRecord_WhenMarkImportFailedRequested_ShouldReturnNotFound()
    {

    }

    [Fact]
    public async Task GivenRecordHasInvalidState_WhenMarkImportFailedRequested_ShouldReturnConflict()
    {

    }

    [Fact]
    public async Task GivenValidRequest_WhenMarkImportFailedRequested_ShouldSucceed()
    {

    }

    // FileImports - Reset

    [Fact]
    public async Task GivenInvalidRequest_WhenResetRequested_ShouldReturnBadRequest()
    {

    }

    [Fact]
    public async Task GivenUnknownRecord_WhenResetRequested_ShouldReturnNotFound()
    {

    }

    [Fact]
    public async Task GivenValidRequest_WhenResetRequested_ShouldSucceed()
    {

    }
}
