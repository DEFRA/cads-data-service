namespace Cads.Cds.BuildingBlocks.Testing.Support.Constants;

public static class TestSqsConstants
{
    public static string TestQueueUrl => $"{TestAwsConstants.AwsServiceUrl.TrimEnd('/')}/000000000000/test-queue";
    public static string TestQueueDlqUrl => $"{TestAwsConstants.AwsServiceUrl.TrimEnd('/')}/000000000000/test-queue-deadletter";

    public const string CadsQueueName = "cads-cds-queue";
    public const string CadsDeadLetterQueueName = "cads-cds-queue-deadletter";

}