namespace Cads.Cds.StorageBridge.Testing.Support.Constants;

public class TestEndpointConstants
{
    // StorageBridge root url
    public const string StorageBridgeRoot = "/api/v1/storage/";

    // S3BulkLoad
    public const string StorageBridgeS3CsvBulkLoadRoot = StorageBridgeRoot + "s3bulkload/csv-import";

    public const string StorageBridgeS3SqlBulkLoadRoot = StorageBridgeRoot + "s3bulkload/sql-import";
}