using Amazon.S3.Model;
using Cads.Cds.ApiSurface.Dtos.Imports;
using Cads.Cds.ApiSurface.Messages.Imports;
using Cads.Cds.BuildingBlocks.Application.Messaging.Models;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.BuildingBlocks.Infrastructure.Messaging.Factories;
using Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Postgres;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;
using Cads.Cds.StorageBridge.Testing.Support.BulkLoad.Utilities;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using FluentAssertions;

namespace Cads.Cds.StorageBridge.Tests.Integration.Consumers;

[Collection("StorageBridgeIntegration"), Trait("Dependence", "testcontainers")]
public class StorageBridgeFifoQueuePollerTests(ApiContainerFixture apiContainerFixture)
{
    private readonly PostgresDb _postgresDb = new(apiContainerFixture.PostgresFixture.HostConnectionString);
    private readonly MessageFactory _messageFactory = new();

    private const int ProcessingTimeCircuitBreakerSeconds = 30;

    [Fact]
    public async Task GivenProcessingS3ToPostgresCopyMessage_WhenMessageHandlerSucceeds_ShouldCompleteMessage()
    {
        var testScenarioUniqueId = new Random().Next(1000, 9999);
        var importFileName
            = $"CTSM_CADS_ENV_BULK_{testScenarioUniqueId}_0001_CT_LOCATIONS_2026-01-01-012345.part-0001.csv";
        var locationsDataRow
            = $"D|2|{testScenarioUniqueId}|1|2|88|N|01-JUL-21|17-JAN-23|BC|AH|Row 2 comments|TL 234567|VT|23456789|0202345678|07723456789|0209876543|email2@internal.test|1|m100000|10-JUN-25|29|AC|1|Y";

        await UploadFileToS3BucketAsync(importFileName, locationsDataRow);

        var testFileImportId = await AddFileImportStatusRecordAsync(importFileName);
        testFileImportId.Should().NotBe(0);

        var metadata = GetFifoMessageMetadata();
        var message = GetMessage(testFileImportId, importFileName);
        var sendRequest = _messageFactory.CreateFifoSqsMessage(apiContainerFixture.LocalStackFixture.CadsFifoQueueUrl!, message, metadata);

        await apiContainerFixture.LocalStackFixture.SqsClient.SendMessageAsync(sendRequest, TestContext.Current.CancellationToken);

        await VerifyLocationAddedAsync(testScenarioUniqueId, locationsDataRow);

        await VerifyFileImportStatusRecordUpdatedAsync(testFileImportId, (int)FileImportStatus.Completed);
    }

    private async Task VerifyLocationAddedAsync(int testScenarioUniqueId, string locationsDataRow)
    {
        var tableName = S3ImportCommandFactory.GetTableName(ImportDataType.CtLocations, SchemaName.CtsTransactions);
        await BulkLoadTestHelpers.AssertCsvRowsMatchDatabaseAsync(
            apiContainerFixture.PostgresFixture.HostConnectionString,
            $"SELECT * FROM {tableName} WHERE loc_id = {testScenarioUniqueId}",
            [
                locationsDataRow
            ]);
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
        var fileData = $"{TestDataFileConstants.LocationsHeader}\n" +
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
         , import_end_at)
         VALUES
             ('dtn', @fileName, 1, NOW(), @fileImportStatus, 1, 1, NULL, NULL)
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

    private static S3ToPostgresCopyMessage GetMessage(long fileImportId, string objectKey) => new()
    {
        FileImportId = fileImportId,
        ObjectKey = objectKey
    };

    private static FifoMessageMetadata GetFifoMessageMetadata()
    {
        return new FifoMessageMetadata(
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString());
    }
}