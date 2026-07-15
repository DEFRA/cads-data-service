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
    private HttpClient _httpClient => _testFixture.HttpClient;

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
            fileName: FileImportDataFactory.New_Scenario_Pending_FileName,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await FileImportTestClient.ReadDtoAsync(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto.FileName.Should().Be(FileImportDataFactory.New_Scenario_Pending_FileName);

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
            FileName = FileImportDataFactory.New_Scenario_Complete_FileName,
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
        problemDetails.Detail.Should().NotBeNull().And.Be($"A record exists with matching file name. ImportStatus: '{FileImportStatus.Complete}'. ProcessingStatus: '{FileProcessingStatus.Pending}'.");
    }

    [Fact]
    public async Task GivenValidBulkRequest_WhenCreateRequested_ShouldSucceed()
    {
        var request = new CreateFileImportRequest
        {
            FileName = FileImportDataFactory.New_Scenario_Create_Bulk_FileName,
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
        FileImportAssertions.ShouldBePending(dto);
    }

    [Fact]
    public async Task GivenValidDeltaRequest_WhenCreateRequested_ShouldSucceed()
    {
        var request = new CreateFileImportRequest
        {
            FileName = FileImportDataFactory.New_Scenario_Create_Delta_FileName,
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
        FileImportAssertions.ShouldBePending(dto);
    }

    [Fact]
    public async Task GivenInvalidDeltaRequest_WhenCreateRequested_ShouldReturnUnprocessableEntity()
    {
        var request = new CreateFileImportRequest
        {
            FileName = FileImportDataFactory.New_Scenario_Create_Invalid_FileName,
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
            ImportStatus = FileImportStatus.Importing
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
            FileImportDataFactory.New_Scenario_Complete_FileName,
            TestContext.Current.CancellationToken);

        var request = new UpdateFileImportRequest
        {
            TotalRowsToProcess = 100,
            RowsFound = 100,
            ImportStatus = FileImportStatus.Importing
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
            FileImportDataFactory.New_Scenario_Importing_FileName,
            TestContext.Current.CancellationToken);

        var request = new UpdateFileImportRequest
        {
            TotalRowsToProcess = 220,
            RowsFound = 210,
            ImportStatus = FileImportStatus.Importing
        };

        var response = await FileImportTestClient.UpdateAsync(
            _httpClient,
            id,
            request,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        await FileImportTestClient.VerifyFileImportAsync(
            _httpClient,
            fileName: FileImportDataFactory.New_Scenario_Importing_FileName,
            dto =>
            {
                FileImportAssertions.ShouldBeImporting(dto);
                FileImportAssertions.ShouldBeTotalRowsToProcess(dto, 220);
                FileImportAssertions.ShouldBeRowsFound(dto, 210);
            },
            TestContext.Current.CancellationToken);
    }

    // FileImports - MarkImporting

    [Fact]
    public async Task GivenInvalidRequest_WhenMarkImportingRequested_ShouldReturnBadRequest()
    {
        var response = await FileImportTestClient.MarkImportingAsync(
            _httpClient,
            id: 0,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownRecord_WhenMarkImportingRequested_ShouldReturnNotFound()
    {
        var response = await FileImportTestClient.MarkImportingAsync(
            _httpClient,
            id: 99,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenRecordHasInvalidState_WhenMarkImportingRequested_ShouldReturnConflict()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _httpClient,
            FileImportDataFactory.New_Scenario_Complete_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.MarkImportingAsync(
            _httpClient,
            id,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task GivenValidRequest_WhenMarkImportingRequested_ShouldSucceed()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _httpClient,
            FileImportDataFactory.New_Scenario_MarkImporting_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.MarkImportingAsync(
            _httpClient,
            id,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        await FileImportTestClient.VerifyFileImportAsync(
            _httpClient,
            fileName: FileImportDataFactory.New_Scenario_MarkImporting_FileName,
            dto =>
            {
                FileImportAssertions.ShouldBeImporting(dto);
            },
            TestContext.Current.CancellationToken);
    }

    // FileImports - MarkImportComplete

    [Fact]
    public async Task GivenInvalidRequest_WhenMarkImportCompleteRequested_ShouldReturnBadRequest()
    {
        var response = await FileImportTestClient.MarkImportCompleteAsync(
            _httpClient,
            id: 0,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownRecord_WhenMarkImportCompleteRequested_ShouldReturnNotFound()
    {
        var response = await FileImportTestClient.MarkImportCompleteAsync(
            _httpClient,
            id: 99,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenRecordHasInvalidState_WhenMarkImportCompleteRequested_ShouldReturnConflict()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _httpClient,
            FileImportDataFactory.New_Scenario_Pending_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.MarkImportCompleteAsync(
            _httpClient,
            id,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task GivenValidRequest_WhenMarkImportCompleteRequested_ShouldSucceed()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _httpClient,
            FileImportDataFactory.New_Scenario_MarkImportComplete_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.MarkImportCompleteAsync(
            _httpClient,
            id,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        await FileImportTestClient.VerifyFileImportAsync(
            _httpClient,
            fileName: FileImportDataFactory.New_Scenario_MarkImportComplete_FileName,
            dto =>
            {
                FileImportAssertions.ShouldBeComplete(dto);
            },
            TestContext.Current.CancellationToken);
    }

    // FileImports - MarkImportFailed

    [Fact]
    public async Task GivenInvalidRequest_WhenMarkImportFailedRequested_ShouldReturnBadRequest()
    {
        var response = await FileImportTestClient.MarkImportFailedAsync(
            _httpClient,
            id: 0,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownRecord_WhenMarkImportFailedRequested_ShouldReturnNotFound()
    {
        var response = await FileImportTestClient.MarkImportFailedAsync(
            _httpClient,
            id: 99,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenRecordHasInvalidState_WhenMarkImportFailedRequested_ShouldReturnConflict()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _httpClient,
            FileImportDataFactory.New_Scenario_Pending_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.MarkImportFailedAsync(
            _httpClient,
            id,
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task GivenValidRequest_WhenMarkImportFailedRequested_ShouldSucceed()
    {
        var id = await FileImportTestClient.GetIdByFileNameAsync(
            _httpClient,
            FileImportDataFactory.New_Scenario_MarkImportFailed_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.MarkImportFailedAsync(
            _httpClient,
            id,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        await FileImportTestClient.VerifyFileImportAsync(
            _httpClient,
            fileName: FileImportDataFactory.New_Scenario_MarkImportFailed_FileName,
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
            FileImportDataFactory.New_Scenario_Reset_FileName,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.ResetAsync(
            _httpClient,
            id,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        await FileImportTestClient.VerifyFileImportAsync(
            _httpClient,
            fileName: FileImportDataFactory.New_Scenario_Reset_FileName,
            dto =>
            {
                FileImportAssertions.ShouldBeReset(dto);
            },
            TestContext.Current.CancellationToken);
    }
}