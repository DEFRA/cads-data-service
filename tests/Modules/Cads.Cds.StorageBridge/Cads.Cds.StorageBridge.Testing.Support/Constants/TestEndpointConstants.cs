namespace Cads.Cds.StorageBridge.Testing.Support.Constants;

public class TestEndpointConstants
{
    // StorageBridge root url
    public const string StorageBridgeRoot = "/api/v1/storage/";

    // S3BulkLoad
    public const string StorageBridgeS3CsvImportRoot = StorageBridgeRoot + "s3Import/csv-import";

    public const string StorageBridgeS3SqlImportRoot = StorageBridgeRoot + "s3Import/sql-import";
}