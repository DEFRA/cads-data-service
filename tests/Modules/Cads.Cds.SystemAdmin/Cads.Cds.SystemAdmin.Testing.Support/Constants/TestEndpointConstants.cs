namespace Cads.Cds.SystemAdmin.Testing.Support.Constants;

public class TestEndpointConstants
{
    // SystemAdmin root url
    public const string SystemAdminRoot = "/api/v1/systemadmin/";

    // FileImports

    // FileImports route paths
    public const string SystemAdminFileImportsRoot = SystemAdminRoot + "fileimports";

    // FileImports - GetByFileName
    public const string FileImportsGetByFileNameEndpoint = SystemAdminFileImportsRoot + "/search";

    // FileImports - Create
    public const string FileImportsCreateEndpoint = SystemAdminFileImportsRoot;

    // FileImports - Update
    public const string FileImportsUpdateEndpoint = SystemAdminFileImportsRoot + "/{0}";

    // FileImports - MarkTransferred
    public const string FileImportsTransferredEndpoint = SystemAdminFileImportsRoot + "/{0}/transferred";

    // FileImports - MarkSplit
    public const string FileImportsSplitEndpoint = SystemAdminFileImportsRoot + "/{0}/split";

    // FileImports - MarkImportComplete
    public const string FileImportsCompleteEndpoint = SystemAdminFileImportsRoot + "/{0}/complete";

    // FileImports - MarkImportFailed
    public const string FileImportsFailedEndpoint = SystemAdminFileImportsRoot + "/{0}/fail";

    // FileImports - Reset
    public const string FileImportsResetEndpoint = SystemAdminFileImportsRoot + "/{0}/reset";
}