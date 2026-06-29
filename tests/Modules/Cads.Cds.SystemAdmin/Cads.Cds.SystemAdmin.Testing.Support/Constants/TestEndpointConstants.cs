namespace Cads.Cds.SystemAdmin.Testing.Support.Constants;

public class TestEndpointConstants
{
    // SystemAdmin root url
    public const string SystemAdminRoot = "/api/v1/systemadmin/";

    // FileImports

    // FileImports route paths
    public const string SystemAdminFileImportsRoot = SystemAdminRoot + "fileimports";

    // FileImports - GetByFileName
    public const string FileImportsGetByFileNameEndpoint = SystemAdminFileImportsRoot + "/by-file-name";

    // FileImports - Create
    public const string FileImportsCreateEndpoint = SystemAdminFileImportsRoot;

    // FileImports - MarkImporting
    public const string FileImportsImportingEndpoint = SystemAdminFileImportsRoot + "/{0}/importing";

    // FileImports - MarkImportComplete
    public const string FileImportsCompleteEndpoint = SystemAdminFileImportsRoot + "/{0}/complete";

    // FileImports - MarkImportFailed
    public const string FileImportsFailedEndpoint = SystemAdminFileImportsRoot + "/{0}/fail";

    // FileImports - Reset
    public const string FileImportsResetEndpoint = SystemAdminFileImportsRoot + "/{0}/reset";
}
