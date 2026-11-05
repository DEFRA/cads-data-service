namespace Cads.Cds.BuildingBlocks.Testing.Support.Constants;

public static class TestSqsConstants
{
    public static string TestQueueUrl => $"{TestAwsConstants.AwsServiceUrl.TrimEnd('/')}/000000000000/test-queue";
    public static string TestQueueDlqUrl => $"{TestAwsConstants.AwsServiceUrl.TrimEnd('/')}/000000000000/test-queue-deadletter";

    public const string CadsFifoQueueName = "cads-cds-queue.fifo";
    public const string CadsFifoDeadLetterQueueName = "cads-cds-queue-deadletter.fifo";

    // Dedicated FIFO queue for async MI report generation (MiBff outbox dispatcher / poller),
    // kept isolated from the shared cads-cds queue so slow report processing can't block imports.
    public const string CadsMiReportsFifoQueueName = "cads-mi-reports-queue.fifo";
    public const string CadsMiReportsFifoDeadLetterQueueName = "cads-mi-reports-queue-deadletter.fifo";

    // Standard (non-FIFO) queue, dedicated to exercising generic SQS admin tooling
    // (peek/metrics/replay) in integration tests. FIFO queues combined with the
    // VisibilityTimeout=0 "peek" semantics used by SqsAdminService are not reliably
    // supported by LocalStack's SQS emulation, so admin tooling is tested against a
    // standard queue instead of the shared FIFO queue used by StorageBridge.
    public const string CadsStandardQueueName = "cads-cds-admin-queue";
    public const string CadsStandardDeadLetterQueueName = "cads-cds-admin-queue-deadletter";
}