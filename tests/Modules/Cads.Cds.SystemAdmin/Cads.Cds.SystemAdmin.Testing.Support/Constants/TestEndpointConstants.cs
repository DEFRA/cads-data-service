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

    // FileImports - GetAll
    public const string FileImportsGetAllEndpoint = SystemAdminFileImportsRoot;

    // FileImports - GetById
    public const string FileImportsGetByIdEndpoint = SystemAdminFileImportsRoot + "/{0}";

    // FileImports - GetByIdWithSiblings    
    public const string FileImportsGetByIdWithSiblingsEndpoint = SystemAdminFileImportsRoot + "/{0}/group";

    // FileImports - Create
    public const string FileImportsCreateEndpoint = SystemAdminFileImportsRoot;

    // FileImports - Update
    public const string FileImportsUpdateEndpoint = SystemAdminFileImportsRoot + "/{0}";

    // FileImports - Batch Update
    public const string FileImportsBatchUpdateEndpoint = SystemAdminFileImportsRoot + "/batch";

    // FileImports - MarkTransferred
    public const string FileImportsTransferredEndpoint = SystemAdminFileImportsRoot + "/{0}/transferred";

    // FileImports - MarkSplit
    public const string FileImportsSplitEndpoint = SystemAdminFileImportsRoot + "/{0}/split";

    // FileImports - MarkCompleted
    public const string FileImportsCompleteEndpoint = SystemAdminFileImportsRoot + "/{0}/completed";

    // FileImports - MarkFailed
    public const string FileImportsFailedEndpoint = SystemAdminFileImportsRoot + "/{0}/failed";

    // FileImports - Reset
    public const string FileImportsResetEndpoint = SystemAdminFileImportsRoot + "/{0}/reset";

    // Generation route paths
    public const string SystemAdminGenerationRoot = SystemAdminRoot + "generation";

    // Generation - Create
    public const string GenerationCreateEndpoint = SystemAdminGenerationRoot;

    // DbAdmin - ExecuteCommand
    public const string DbAdminExecuteCommandEndpoint = SystemAdminRoot + "db-admin-execute-command";

    // DbAdmin - CtsImport
    public const string DbAdminCtsImportEndpoint = SystemAdminRoot + "db-admin-cts-import";

    // Generation - Scenarios
    public const string GenerationGetScenariosEndpoint = SystemAdminGenerationRoot + "/scenarios";

    // SqsAdmin - route paths
    public const string SqsAdminQueuesRoot = SystemAdminRoot + "sqs/queues";

    // SqsAdmin - GetQueues
    public const string SqsAdminGetQueuesEndpoint = SqsAdminQueuesRoot;

    // SqsAdmin - GetMetrics
    public const string SqsAdminGetMetricsEndpoint = SqsAdminQueuesRoot + "/{0}/metrics";

    // SqsAdmin - GetMessages
    public const string SqsAdminGetMessagesEndpoint = SqsAdminQueuesRoot + "/{0}/messages";

    // SqsAdmin - GetDlqMessages
    public const string SqsAdminGetDlqMessagesEndpoint = SqsAdminQueuesRoot + "/{0}/dlq/messages";

    // SqsAdmin - GetDlqMetrics
    public const string SqsAdminGetDlqMetricsEndpoint = SqsAdminQueuesRoot + "/{0}/dlq/metrics";

    // SqsAdmin - ReplayDlq
    public const string SqsAdminReplayDlqEndpoint = SqsAdminQueuesRoot + "/{0}/dlq/replay";
}