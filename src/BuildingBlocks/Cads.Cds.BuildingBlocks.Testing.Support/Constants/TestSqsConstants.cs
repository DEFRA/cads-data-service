namespace Cads.Cds.BuildingBlocks.Testing.Support.Constants;

public static class TestSqsConstants
{
    public static string TestQueueUrl => $"{TestAwsConstants.AwsServiceUrl.TrimEnd('/')}/000000000000/test-queue";
    public static string TestQueueDlqUrl => $"{TestAwsConstants.AwsServiceUrl.TrimEnd('/')}/000000000000/test-queue-deadletter";

    public const string CadsFifoQueueName = "cads-cds-queue.fifo";
    public const string CadsFifoDeadLetterQueueName = "cads-cds-queue-deadletter.fifo";

    // Standard (non-FIFO) queue, dedicated to exercising generic SQS admin tooling
    // (peek/metrics/replay) in integration tests. FIFO queues combined with the
    // VisibilityTimeout=0 "peek" semantics used by SqsAdminService are not reliably
    // supported by LocalStack's SQS emulation, so admin tooling is tested against a
    // standard queue instead of the shared FIFO queue used by StorageBridge.
    public const string CadsStandardQueueName = "cads-cds-admin-queue";
    public const string CadsStandardDeadLetterQueueName = "cads-cds-admin-queue-deadletter";
}