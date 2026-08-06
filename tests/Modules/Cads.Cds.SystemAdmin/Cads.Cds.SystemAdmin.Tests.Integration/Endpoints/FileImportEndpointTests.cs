using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.SystemAdmin.Controllers.Requests.Imports;
using Cads.Cds.SystemAdmin.Testing.Support.ApiClients;
using FluentAssertions;
using System.Net;

namespace Cads.Cds.SystemAdmin.Tests.Integration.Endpoints;

[Collection("SystemAdminIntegration"), Trait("Dependence", "testcontainers")]
public class FileImportEndpointTests(ApiContainerFixture apiContainerFixture)
{
    private HttpClient _httpClient => apiContainerFixture.CreateBasicClient();

    // FileImports - GetByFileName

    [Fact]
    public async Task GivenInvalidRequest_WhenGetByFileNameRequested_ShouldReturnBadRequest()
    {
        var response = await FileImportTestClient.GetByFileNameAsync(
            _httpClient,
            fileName: null,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownFileName_WhenGetByFileNameRequested_ShouldReturnNotFound()
    {
        var response = await FileImportTestClient.GetByFileNameAsync(
            _httpClient,
            fileName: "unknownFileName",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenValidFileName_WhenGetByFileNameRequested_ShouldSucceed()
    {
        var response = await FileImportTestClient.GetByFileNameAsync(
            _httpClient,
            fileName: TestFileScenarioConstants.New_Scenario_Pending_FileName,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await FileImportTestClient.ReadDtoAsync(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto.FileName.Should().Be(TestFileScenarioConstants.New_Scenario_Pending_FileName);

        FileImportAssertions.ShouldBePending(dto);
    }

    // FileImports - Create

    [Fact]
    public async Task GivenInvalidRequest_WhenCreateRequested_ShouldReturnBadRequest()
    {
        var response = await FileImportTestClient.CreateAsync(
            _httpClient,
            request: new CreateFileImportRequest(),
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenFileNameAlreadyExists_WhenCreateRequested_ShouldReturnConflict()
    {
        var request = new CreateFileImportRequest
        {
            FileName = TestFileScenarioConstants.New_Scenario_Complete_FileName,
            TotalRowsToProcess = 100,
            RowsFound = 0
        };

        var response = await FileImportTestClient.CreateAsync(
            _httpClient,
            request,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);

        var problemDetails = await FileImportTestClient.ReadProblemDetailsAsync(
            response,
            TestContext.Current.CancellationToken);

        problemDetails.Should().NotBeNull();
        problemDetails.Detail.Should().NotBeNull().And.Be($"A record exists with matching file name. ImportStatus: '{FileImportStatus.Completed}'. ProcessingStatus: '{FileProcessingStatus.Pending}'.");
    }

    [Fact]
    public async Task GivenValidBulkRequest_WhenCreateRequested_ShouldSucceed()
    {
        var request = new CreateFileImportRequest
        {
            FileName = TestFileScenarioConstants.New_Scenario_Create_Bulk_FileName,
            TotalRowsToProcess = 100,
            RowsFound = 0
        };

        var response = await FileImportTestClient.CreateAsync(
            _httpClient,
            request,
            TestContext.Current.CancellationToken);

        var debugContent = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.IsSuccessStatusCode.Should().BeTrue($"status={(int)response.StatusCode} body={debugContent}");

        var dto = await FileImportTestClient.ReadDtoAsync(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto.Id.Should().BeGreaterThan(0);
        FileImportAssertions.ShouldBePending(dto);
    }

    [Fact]
    public async Task GivenValidDeltaRequest_WhenCreateRequested_ShouldSucceed()
    {
        var request = new CreateFileImportRequest
        {
            FileName = TestFileScenarioConstants.New_Scenario_Create_Delta_FileName,
            TotalRowsToProcess = 100,
            RowsFound = 0
        };

        var response = await FileImportTestClient.CreateAsync(
            _httpClient,
            request,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await FileImportTestClient.ReadDtoAsync(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto.Id.Should().BeGreaterThan(0);
        FileImportAssertions.ShouldBePending(dto);
    }

    [Fact]
    public async Task GivenInvalidDeltaRequest_WhenCreateRequested_ShouldReturnUnprocessableEntity()
    {
        var request = new CreateFileImportRequest
        {
            FileName = TestFileScenarioConstants.New_Scenario_Create_Invalid_FileName,
            TotalRowsToProcess = 100,
            RowsFound = 0
        };

        var response = await FileImportTestClient.CreateAsync(
            _httpClient,
            request,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
    }

    // FileImports - Update

    [Fact]
    public async Task GivenInvalidRequest_WhenUpdateRequested_ShouldReturnBadRequest()
    {
        var response = await FileImportTestClient.UpdateAsync(
            _httpClient,
            id: 0,
            request: new UpdateFileImportRequest(),
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownRecord_WhenUpdateRequested_ShouldReturnNotFound()
    {
        var request = new UpdateFileImportRequest
        {
            TotalRowsToProcess = 100,
            RowsFound = 0,
            ImportStatus = FileImportStatus.Transferred
        };

        var response = await FileImportTestClient.UpdateAsync(
            _httpClient,
            id: 99,
            request,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenRecordHasInvalidState_WhenUpdateRequested_ShouldReturnConflict()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _httpClient,
            TestFileScenarioConstants.New_Scenario_Complete_FileName,
            TestContext.Current.CancellationToken);

        var request = new UpdateFileImportRequest
        {
            TotalRowsToProcess = 100,
            RowsFound = 100,
            ImportStatus = FileImportStatus.Transferred
        };

        var response = await FileImportTestClient.UpdateAsync(
            _httpClient,
            id,
            request,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task GivenValidRequest_WhenUpdateRequested_ShouldSucceed()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _httpClient,
            TestFileScenarioConstants.New_Scenario_Transferred_FileName,
            TestContext.Current.CancellationToken);

        var request = new UpdateFileImportRequest
        {
            TotalRowsToProcess = 220,
            RowsFound = 210,
            ImportStatus = FileImportStatus.Transferred
        };

        var response = await FileImportTestClient.UpdateAsync(
            _httpClient,
            id,
            request,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        await FileImportTestClient.VerifyFileImportAsync(
            _httpClient,
            fileName: TestFileScenarioConstants.New_Scenario_Transferred_FileName,
            dto =>
            {
                FileImportAssertions.ShouldBeTransferred(dto);
                FileImportAssertions.ShouldBeTotalRowsToProcess(dto, 220);
                FileImportAssertions.ShouldBeRowsFound(dto, 210);
            },
            TestContext.Current.CancellationToken);
    }

    // FileImports - MarkFailed

    [Fact]
    public async Task GivenInvalidRequest_WhenMarkFailedRequested_ShouldReturnBadRequest()
    {
        var response = await FileImportTestClient.MarkFailedAsync(
            _httpClient,
            id: 0,
            reason: "invalid request",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownRecord_WhenMarkFailedRequested_ShouldReturnNotFound()
    {
        var response = await FileImportTestClient.MarkFailedAsync(
            _httpClient,
            id: 99,
            reason: "not found",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenRecordHasCompleteState_WhenMarkFailedRequested_ShouldReturnConflict()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _httpClient,
            TestFileScenarioConstants.New_Scenario_Complete_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.MarkFailedAsync(
            _httpClient,
            id,
            reason: "this is a conflict",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task GivenValidRequest_WhenMarkFailedRequested_ShouldSucceed()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _httpClient,
            TestFileScenarioConstants.New_Scenario_MarkImportFailed_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.MarkFailedAsync(
            _httpClient,
            id,
            reason: "error during file import",
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        await FileImportTestClient.VerifyFileImportAsync(
            _httpClient,
            fileName: TestFileScenarioConstants.New_Scenario_MarkImportFailed_FileName,
            dto =>
            {
                FileImportAssertions.ShouldBeFailed(dto);
            },
            TestContext.Current.CancellationToken);
    }

    // FileImports - Reset

    [Fact]
    public async Task GivenInvalidRequest_WhenResetRequested_ShouldReturnBadRequest()
    {
        var response = await FileImportTestClient.ResetAsync(
            _httpClient,
            id: 0,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownRecord_WhenResetRequested_ShouldReturnNotFound()
    {
        var response = await FileImportTestClient.ResetAsync(
            _httpClient,
            id: 99,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenValidRequest_WhenResetRequested_ShouldSucceed()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _httpClient,
            TestFileScenarioConstants.New_Scenario_Reset_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.ResetAsync(
            _httpClient,
            id,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        await FileImportTestClient.VerifyFileImportAsync(
            _httpClient,
            fileName: TestFileScenarioConstants.New_Scenario_Reset_FileName,
            dto =>
            {
                FileImportAssertions.ShouldBeReset(dto);
            },
            TestContext.Current.CancellationToken);
    }
}