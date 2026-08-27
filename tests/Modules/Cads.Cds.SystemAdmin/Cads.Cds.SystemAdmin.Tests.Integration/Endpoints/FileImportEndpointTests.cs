using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;
using Cads.Cds.SystemAdmin.Controllers.Requests.Imports;
using Cads.Cds.SystemAdmin.Testing.Support.ApiClients;
using FluentAssertions;
using System.Net;

namespace Cads.Cds.SystemAdmin.Tests.Integration.Endpoints;

[Collection("SystemAdminIntegration"), Trait("Dependence", "testcontainers")]
public class FileImportEndpointTests(ApiContainerFixture apiContainerFixture)
{
    private HttpClient _httpClient => apiContainerFixture.CreateBasicClient();

    private readonly PostgresDb _postgresDb = new(apiContainerFixture.PostgresFixture.HostConnectionString);

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
        problemDetails.Detail.Should().NotBeNull().And.Be($"A record exists with matching file name '{request.FileName}'.");
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
    public async Task GivenValidRequest_WithImportTable_NoCorrespondingDestinationTable_WhenCreateRequested_ShouldFailed()
    {
        var request = new CreateFileImportRequest
        {
            FileName = TestFileScenarioConstants.New_Scenario_Create_NoDestinationTable_FileName,
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
        FileImportAssertions.ShouldBeFailedWithUnknownDestinationTableName(dto);
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
        var testGroupKey = "CTSM_CADS_PROD_BULK_555_CT_LOCATIONS";
        var testFileName = "CTSM_CADS_PROD_BULK_555_0001_CT_LOCATIONS_2026-01-01-012345.CSV";

        await _postgresDb.DeleteFileImportByGroupKeyAsync(testGroupKey);

        var recordId = await _postgresDb.InsertFileImportAsync(testFileName, testGroupKey, FileImportStatus.Pending);

        var request = new UpdateFileImportRequest
        {
            TotalRowsToProcess = 220,
            RowsFound = 210,
            ImportStatus = FileImportStatus.Transferred
        };

        var response = await FileImportTestClient.UpdateAsync(
            _httpClient,
            recordId,
            request,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        await FileImportTestClient.VerifyFileImportAsync(
            _httpClient,
            fileName: testFileName,
            dto =>
            {
                FileImportAssertions.ShouldBeTransferred(dto);
                FileImportAssertions.ShouldBeTotalRowsToProcess(dto, 220);
                FileImportAssertions.ShouldBeRowsFound(dto, 210);
            },
            TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task GivenValidRequest_WhenBatchUpdateRequested_ShouldSucceed()
    {
        var testGroupKey = "CTSM_CADS_PROD_BULK_666_CT_LOCATIONS";
        var testFileNameTemplateWithIndex = "CTSM_CADS_PROD_BULK_666_{0:D4}_CT_LOCATIONS_2026-01-01-012345.CSV";
        var testFileImportCount = 10;

        await _postgresDb.DeleteFileImportByGroupKeyAsync(testGroupKey);

        var recordIds = await AddFileImportRecordsAsync(testFileImportCount, testFileNameTemplateWithIndex, testGroupKey, FileImportStatus.Pending);

        var request = new BatchUpdateFileImportRequest
        {
            GroupKey = testGroupKey,
            TotalRowsToProcess = 220,
            RowsFound = 210,
            ImportStatus = FileImportStatus.Transferred
        };

        var response = await FileImportTestClient.BatchUpdateAsync(
            _httpClient,
            request,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await _postgresDb.ExecuteQueryAsync("SELECT * FROM cads.cts_file_imports WHERE group_key = @groupKey",
            reader => new FileImportDto
            {
                Id = (Int64)reader["cts_file_import_id"],
                DestinationTableName = (string)reader["destination_table_name"],
                FileName = (string)reader["file_name"],
                GroupKey = (string)reader["group_key"],
                TotalRowsToProcess = (Int64)reader["total_rows_to_process"],
                RowsFound = (Int64)reader["rows_found"],
                ImportStatus = (FileImportStatus)(Int16)reader["import_status_id"]
            }, cmd => cmd.Parameters.AddWithValue("groupKey", testGroupKey));

        dto.Should().NotBeNull();
        dto.Should().HaveCount(testFileImportCount);

        var index = 0;

        foreach (var item in dto)
        {
            index++;
            item.GroupKey.Should().Be(testGroupKey);
            item.FileName.Should().Be(string.Format(testFileNameTemplateWithIndex, index));
            item.ImportStatus.Should().Be(FileImportStatus.Transferred);
            FileImportAssertions.ShouldBeTotalRowsToProcess(item, 220);
            FileImportAssertions.ShouldBeRowsFound(item, 210);
        }
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
    public async Task GivenValidRequest_WhenGetByIdRequested_ShouldSucceed()
    {
        var testFileNameTemplateWithIndex = "CTSM_CADS_PROD_BULK_777_{0:D4}_CT_LOCATIONS_2026-01-01-012345.CSV";
        var testGroupKey = "CTSM_CADS_PROD_BULK_777_CT_LOCATIONS";
        var testFileImportCount = 1;

        await _postgresDb.DeleteFileImportByGroupKeyAsync(testGroupKey);

        var recordIds = await AddFileImportRecordsAsync(testFileImportCount, testFileNameTemplateWithIndex, testGroupKey, FileImportStatus.Pending);

        // Reset records to Split status for testing GetByIdAsync
        var request = new BatchUpdateFileImportRequest
        {
            GroupKey = testGroupKey,
            TotalRowsToProcess = 0,
            RowsFound = 0,
            ImportStatus = FileImportStatus.Split
        };

        var batchResponse = await FileImportTestClient.BatchUpdateAsync(
            _httpClient,
            request,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.GetByIdAsync(
            _httpClient,
            id: recordIds[0],
            cancellationToken: TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await FileImportTestClient.ReadDtoAsync<FileImportDto>(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto.Id.Should().Be(recordIds[0]);
        dto.GroupKey.Should().Be(testGroupKey);
        dto.FileName.Should().Be(string.Format(testFileNameTemplateWithIndex, 1));
        dto.ImportStatus.Should().Be(FileImportStatus.Split);
    }

    [Fact]
    public async Task GivenValidRequest_WhenGetByIdWithSiblingsRequested_ShouldSucceed()
    {
        var testFileNameTemplateWithIndex = "CTSM_CADS_PROD_BULK_888_{0:D4}_CT_LOCATIONS_2026-01-01-012345.CSV";
        var testGroupKey = "CTSM_CADS_PROD_BULK_888_CT_LOCATIONS";
        var testFileImportCount = 10;

        await _postgresDb.DeleteFileImportByGroupKeyAsync(testGroupKey);

        var recordIds = await AddFileImportRecordsAsync(testFileImportCount, testFileNameTemplateWithIndex, testGroupKey, FileImportStatus.Pending);

        // Reset records to Split status for testing GetByIdWithSiblingsAsync
        var request = new BatchUpdateFileImportRequest
        {
            GroupKey = testGroupKey,
            TotalRowsToProcess = 0,
            RowsFound = 0,
            ImportStatus = FileImportStatus.Split
        };

        var batchResponse = await FileImportTestClient.BatchUpdateAsync(
            _httpClient,
            request,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.GetByIdWithSiblingsAsync(
            _httpClient,
            id: recordIds[0],
            cancellationToken: TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await FileImportTestClient.ReadDtoAsync<List<FileImportDto>>(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto.Should().HaveCount(testFileImportCount);

        var index = 0;

        foreach (var item in dto)
        {
            index++;
            item.GroupKey.Should().Be(testGroupKey);
            item.FileName.Should().Be(string.Format(testFileNameTemplateWithIndex, index));
            item.ImportStatus.Should().Be(FileImportStatus.Split);
        }
    }

    [Fact]
    public async Task GivenValidGroupedFiles_WhenGetAllRequested_ShouldSucceed()
    {
        var testFileNameTemplateWithIndex = "CTSM_CADS_PROD_BULK_999_{0:D4}_CT_LOCATIONS_2026-01-01-012345.CSV";
        var testGroupKey = "CTSM_CADS_PROD_BULK_999_CT_LOCATIONS";
        var testFileImportCount = 10;

        await _postgresDb.DeleteFileImportByGroupKeyAsync(testGroupKey);

        var recordIds = await AddFileImportRecordsAsync(testFileImportCount, testFileNameTemplateWithIndex, testGroupKey, FileImportStatus.Pending);

        // Reset records to Split status for testing GetAllAsync
        var request = new BatchUpdateFileImportRequest
        {
            GroupKey = testGroupKey,
            TotalRowsToProcess = 0,
            RowsFound = 0,
            ImportStatus = FileImportStatus.Split
        };

        var batchResponse = await FileImportTestClient.BatchUpdateAsync(
            _httpClient,
            request,
            TestContext.Current.CancellationToken);

        var response = await FileImportTestClient.GetAllAsync(
            _httpClient,
            groupKey: testGroupKey,
            cancellationToken: TestContext.Current.CancellationToken);

        var dataset = await _postgresDb.GetFileImportDataSetByGroupKey(testGroupKey);

        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await FileImportTestClient.ReadDtoAsync<List<FileImportDto>>(
            response,
            TestContext.Current.CancellationToken);

        dto.Should().NotBeNull();
        dto.Should().HaveCount(testFileImportCount);

        var index = 0;

        foreach (var item in dto)
        {
            index++;
            item.GroupKey.Should().Be(testGroupKey);
            item.FileName.Should().Be(string.Format(testFileNameTemplateWithIndex, index));
            item.ImportStatus.Should().Be(FileImportStatus.Split);
        }
    }

    private async Task<List<long>> AddFileImportRecordsAsync(int range, string filenameTemplate, string testGroupKey, FileImportStatus fileImportStatus, string? lastFilePartImported = null, long rowsImported = 0)
    {
        var list = new List<long>();

        for (var index = 1; index <= range; index++)
        {
            var testFilename = string.Format(filenameTemplate, index);
            var id = await _postgresDb.InsertFileImportAsync(testFilename, testGroupKey, fileImportStatus, lastFilePartImported, rowsImported);
            list.Add(id);
        }

        return list;
    }
}