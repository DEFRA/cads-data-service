using Amazon.S3.Model;
using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;
using Cads.Cds.SystemAdmin.Controllers.Requests.Imports;
using Cads.Cds.SystemAdmin.Testing.Support.ApiClients;
using FluentAssertions;

namespace Cads.Cds.SystemAdmin.Tests.Integration.Endpoints;

[Collection("SystemAdminIntegration"), Trait("Dependence", "testcontainers")]
public class FileImportWithMessageProcessingTests(ApiContainerFixture apiContainerFixture)
{
    private HttpClient _httpClient => apiContainerFixture.CreateBasicClient();

    private readonly PostgresDb _postgresDb = new(apiContainerFixture.PostgresFixture.HostConnectionString);

    private static string s_locationsHeader =>
        "record_type|record_count|loc_id|loc_slt_id|loc_lty_id|loc_cty_id|loc_receive_labels_flag|loc_effective_from|loc_effective_to|loc_cessation_reason|loc_premises_type|loc_comments|loc_map_reference|loc_source_identifier|loc_source_reference|loc_tel_number|loc_mobile_number|loc_fax_number|loc_email_address|loc_current_status|loc_current_user|loc_current_modified_date|loc_current_pid|loc_reason_code|loc_version|loc_receive_ppaf_flag";

    private const int ProcessingTimeCircuitBreakerSeconds = 30;

    [Fact]
    public async Task GivenFileImportStatusRecord_WhenUpdateToSplitRequested_ShouldSucceedAndProcessImport()
    {
        var testScenarioUniqueId = new Random().Next(1000, 9999);
        var importFileName
            = $"CTSM_CADS_ENV_BULK_{testScenarioUniqueId}_0001_CT_LOCATIONS_2026-01-01-012345.csv";
        var importFileKey = $"import/CTSM_CADS_ENV_BULK_{testScenarioUniqueId}_0001_CT_LOCATIONS_2026-01-01-012345/{importFileName}";
        var locationsDataRow
            = $"D|2|{testScenarioUniqueId}|1|2|88|N|01-JUL-21|17-JAN-23|BC|AH|Row 2 comments|TL 234567|VT|23456789|0202345678|07723456789|0209876543|email2@internal.test|1|m100000|10-JUN-25|29|AC|1|Y";

        await UploadFileToS3BucketAsync(importFileKey, locationsDataRow);

        var testFileImportId = await AddFileImportStatusRecordAsync(importFileName);
        testFileImportId.Should().NotBe(0);

        var request = new UpdateFileImportRequest
        {
            TotalRowsToProcess = 1,
            RowsFound = 1,
            ImportStatus = FileImportStatus.Split
        };

        var response = await FileImportTestClient.UpdateAsync(
            _httpClient,
            testFileImportId,
            request,
            TestContext.Current.CancellationToken);

        response.IsSuccessStatusCode.Should().BeTrue();

        await VerifyLocationAddedAsync(testScenarioUniqueId);

        await VerifyFileImportStatusRecordUpdatedAsync(testFileImportId, (int)FileImportStatus.Completed);
    }

    private async Task VerifyLocationAddedAsync(decimal testScenarioUniqueId)
    {
        var tableName = S3ImportCommandFactory.GetTableName(ImportDataType.CtLocations, SchemaName.CtsTransactions);

        var locationId = await _postgresDb.PollUntilAsync<decimal>(
            $"SELECT loc_id FROM {tableName} WHERE loc_id = @id",
            id => id == testScenarioUniqueId,
            TimeSpan.FromSeconds(ProcessingTimeCircuitBreakerSeconds),
            cmd => cmd.Parameters.AddWithValue("id", testScenarioUniqueId));

        locationId.Should().Be(testScenarioUniqueId);
    }

    private async Task VerifyFileImportStatusRecordUpdatedAsync(long fileImportId, short expected)
    {
        var importStatus = await _postgresDb.PollUntilAsync<short>(
            "SELECT import_status_id FROM cads.cts_file_imports WHERE cts_file_import_id = @id",
            status => status == expected,
            TimeSpan.FromSeconds(ProcessingTimeCircuitBreakerSeconds),
            cmd => cmd.Parameters.AddWithValue("id", fileImportId));

        importStatus.Should().Be(expected);
    }

    private async Task UploadFileToS3BucketAsync(string importFileName, string locationsDataRow)
    {
        var fileData = $"{s_locationsHeader}\n" +
                       $"{locationsDataRow}";

        await apiContainerFixture.LocalStackFixture.S3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = LocalStackFixture.CadsInternalBucketName,
            Key = importFileName,
            ContentBody = fileData
        }, TestContext.Current.CancellationToken);
    }

    private async Task<long> AddFileImportStatusRecordAsync(string importFileName)
    {
        var insertQuery = @"INSERT INTO cads.cts_file_imports(
        destination_table_name
        , file_name
        , total_rows_to_process
        , added_at
        , import_status_id
        , processing_status_id
        , rows_found
        , import_start_at
        , import_end_at
        , batch_date
        , group_key
        , import_type
        , failed_attempts
        , last_error_reason)
         VALUES
             ('dtn', @fileName, 1, NOW(), @fileImportStatus, 1, 1, NULL, NULL, NOW(), 'ABC', 'BULK', 0, NULL)
        RETURNING cts_file_import_id;";

        var testFileImportId = await _postgresDb.ExecuteScalarAsync<long>(
            insertQuery,
            cmd =>
            {
                cmd.Parameters.AddWithValue("fileName", importFileName);
                cmd.Parameters.AddWithValue("fileImportStatus", (int)FileImportStatus.Transferred);
            });

        return testFileImportId;
    }
}