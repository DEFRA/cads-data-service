using Cads.Cds.BuildingBlocks.Core.Domain.Imports;
using Cads.Cds.SystemAdmin.Controllers.Requests.Imports;
using Cads.Cds.SystemAdmin.Testing.Support.ApiClients;
using Cads.Cds.SystemAdmin.Testing.Support.Factories;
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
        var response = await FileImportTestClient.GetByFileNameAsync(
            _testFixture.HttpClient,
            fileName: null, 
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownFileName_WhenGetByFileNameRequested_ShouldReturnNotFound()
    {
        var response = await FileImportTestClient.GetByFileNameAsync(
            _testFixture.HttpClient,
            fileName: "unknownFileName",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenValidFileName_WhenGetByFileNameRequested_ShouldSucceed()
    {
        var response = await FileImportTestClient.GetByFileNameAsync(
            _testFixture.HttpClient,
            fileName: FileImportDataFactory.Scenario_Pending_FileName,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await FileImportTestClient.ReadDtoAsync(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto.FileName.Should().Be(FileImportDataFactory.Scenario_Pending_FileName);

        FileImportAssertions.ShouldBePending(dto);
    }

    // FileImports - Create

    [Fact]
    public async Task GivenInvalidRequest_WhenCreateRequested_ShouldReturnBadRequest()
    {
        var response = await FileImportTestClient.CreateAsync(
            _testFixture.HttpClient,
            request: new CreateFileImportRequest(),
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenValidRequest_WhenCreateRequested_ShouldSucceed()
    {
        var request = new CreateFileImportRequest
        {
            FileName = FileImportDataFactory.Scenario_Create_FileName,
            TotalRowsToProcess = 100,
            RowsFound = 0
        };

        var response = await FileImportTestClient.CreateAsync(
            _testFixture.HttpClient,
            request,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await FileImportTestClient.ReadDtoAsync(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        FileImportAssertions.ShouldBePending(dto);
    }

    // FileImports - MarkImporting

    [Fact]
    public async Task GivenInvalidRequest_WhenMarkImportingRequested_ShouldReturnBadRequest()
    {
        var response = await FileImportTestClient.MarkImportingAsync(
            _testFixture.HttpClient,
            id: 0,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownRecord_WhenMarkImportingRequested_ShouldReturnNotFound()
    {
        var response = await FileImportTestClient.MarkImportingAsync(
            _testFixture.HttpClient,
            id: 99,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenRecordHasInvalidState_WhenMarkImportingRequested_ShouldReturnConflict()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _testFixture.HttpClient,
            FileImportDataFactory.Scenario_Complete_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.MarkImportingAsync(
            _testFixture.HttpClient,
            id,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task GivenValidRequest_WhenMarkImportingRequested_ShouldSucceed()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _testFixture.HttpClient,
            FileImportDataFactory.Scenario_MarkImporting_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.MarkImportingAsync(
            _testFixture.HttpClient,
            id,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        await FileImportTestClient.VerifyFileImportAsync(
            _testFixture.HttpClient,
            fileName: FileImportDataFactory.Scenario_MarkImporting_FileName,
            TestContext.Current.CancellationToken,
            dto =>
            {
                FileImportAssertions.ShouldBeImporting(dto);
            });
    }

    // FileImports - MarkImportComplete

    [Fact]
    public async Task GivenInvalidRequest_WhenMarkImportCompleteRequested_ShouldReturnBadRequest()
    {
        var response = await FileImportTestClient.MarkImportCompleteAsync(
            _testFixture.HttpClient,
            id: 0,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownRecord_WhenMarkImportCompleteRequested_ShouldReturnNotFound()
    {
        var response = await FileImportTestClient.MarkImportCompleteAsync(
            _testFixture.HttpClient,
            id: 99,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenRecordHasInvalidState_WhenMarkImportCompleteRequested_ShouldReturnConflict()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _testFixture.HttpClient,
            FileImportDataFactory.Scenario_Pending_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.MarkImportCompleteAsync(
            _testFixture.HttpClient,
            id,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task GivenValidRequest_WhenMarkImportCompleteRequested_ShouldSucceed()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _testFixture.HttpClient,
            FileImportDataFactory.Scenario_MarkImportComplete_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.MarkImportCompleteAsync(
            _testFixture.HttpClient,
            id,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        await FileImportTestClient.VerifyFileImportAsync(
            _testFixture.HttpClient,
            fileName: FileImportDataFactory.Scenario_MarkImportComplete_FileName,
            TestContext.Current.CancellationToken,
            dto =>
            {
                FileImportAssertions.ShouldBeComplete(dto);
            });
    }

    // FileImports - MarkImportFailed

    [Fact]
    public async Task GivenInvalidRequest_WhenMarkImportFailedRequested_ShouldReturnBadRequest()
    {
        var response = await FileImportTestClient.MarkImportFailedAsync(
            _testFixture.HttpClient,
            id: 0,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownRecord_WhenMarkImportFailedRequested_ShouldReturnNotFound()
    {
        var response = await FileImportTestClient.MarkImportFailedAsync(
            _testFixture.HttpClient,
            id: 99,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenRecordHasInvalidState_WhenMarkImportFailedRequested_ShouldReturnConflict()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _testFixture.HttpClient,
            FileImportDataFactory.Scenario_Pending_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.MarkImportFailedAsync(
            _testFixture.HttpClient,
            id,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task GivenValidRequest_WhenMarkImportFailedRequested_ShouldSucceed()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _testFixture.HttpClient,
            FileImportDataFactory.Scenario_MarkImportFailed_FileName,
            TestContext.Current.CancellationToken);
        
        var response = await FileImportTestClient.MarkImportFailedAsync(
            _testFixture.HttpClient,
            id,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        await FileImportTestClient.VerifyFileImportAsync(
            _testFixture.HttpClient,
            fileName: FileImportDataFactory.Scenario_MarkImportFailed_FileName,
            TestContext.Current.CancellationToken,
            dto =>
            {
                FileImportAssertions.ShouldBeFailed(dto);
            });
    }

    // FileImports - Reset

    [Fact]
    public async Task GivenInvalidRequest_WhenResetRequested_ShouldReturnBadRequest()
    {
        var response = await FileImportTestClient.ResetAsync(
            _testFixture.HttpClient,
            id: 0,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownRecord_WhenResetRequested_ShouldReturnNotFound()
    {
        var response = await FileImportTestClient.ResetAsync(
            _testFixture.HttpClient,
            id: 99,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenValidRequest_WhenResetRequested_ShouldSucceed()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _testFixture.HttpClient,
            FileImportDataFactory.Scenario_Reset_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.ResetAsync(
            _testFixture.HttpClient,
            id,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        await FileImportTestClient.VerifyFileImportAsync(
            _testFixture.HttpClient,
            fileName: FileImportDataFactory.Scenario_Reset_FileName,
            TestContext.Current.CancellationToken,
            dto =>
            {
                FileImportAssertions.ShouldBeReset(dto);
            });
    }
}
