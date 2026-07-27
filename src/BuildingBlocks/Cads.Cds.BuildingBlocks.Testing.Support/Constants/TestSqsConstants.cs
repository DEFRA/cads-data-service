namespace Cads.Cds.BuildingBlocks.Testing.Support.Constants;

public static class TestSqsConstants
{
    public static string TestQueueUrl => $"{TestAwsConstants.AwsServiceUrl.TrimEnd('/')}/000000000000/test-queue";
    public static string TestQueueDlqUrl => $"{TestAwsConstants.AwsServiceUrl.TrimEnd('/')}/000000000000/test-queue-deadletter";

    public const string CadsFifoQueueName = "cads-cds-queue.fifo";
    public const string CadsFifoDeadLetterQueueName = "cads-cds-queue-deadletter.fifo";

}