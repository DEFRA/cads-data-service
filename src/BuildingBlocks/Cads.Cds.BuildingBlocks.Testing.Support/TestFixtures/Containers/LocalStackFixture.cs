using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Testcontainers.LocalStack;
using Xunit;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures.Containers;

// ReSharper disable once ClassNeverInstantiated.Global
public class LocalStackFixture : IAsyncLifetime
{
    public static LocalStackContainer? LocalStackContainer { get; private set; }

    public IAmazonSQS SqsClient { get; private set; } = null!;
    public IAmazonS3 S3Client { get; private set; } = null!;

    public string? SqsEndpoint { get; private set; }

    public static string ServiceUrl => $"http://localhost:{LocalStackContainer!.GetMappedPublicPort(TestContainerConstants.LocalStackPort)}";
    public static string NetworkServiceUrl => $"http://{TestContainerConstants.NetworkAlias}:{TestContainerConstants.LocalStackPort}";
    public static string CadsQueueUrl => $"http://sqs.eu-west-2.localhost.localstack.cloud:{TestContainerConstants.LocalStackPort}/000000000000/{TestSqsConstants.CadsQueueName}";
    public static string CadsDeadLetterQueueUrl => $"http://sqs.eu-west-2.localhost.localstack.cloud:{TestContainerConstants.LocalStackPort}/000000000000/{TestSqsConstants.CadsDeadLetterQueueName}";

    public const string AwsAccessKeyId = "test";
    public const string AwsSecretAccessKey = "test";
    public const string AuthenticationRegion = TestAwsConstants.AwsRegion;
    public const string CadsInternalBucketName = TestS3Constants.TestCadsInternalBucketName;
    public const string CadsExternalBucketName = TestS3Constants.TestCadsExternalBucketName;

    private static Amazon.Runtime.BasicAWSCredentials GetBasicAWSCredentials => new(AwsAccessKeyId, AwsSecretAccessKey);

    public async ValueTask InitializeAsync()
    {
        DockerNetworkHelper.EnsureNetworkExists(TestContainerConstants.NetworkName);

        LocalStackContainer = new LocalStackBuilder("localstack/localstack:3.0.2")
            .WithEnvironment("SERVICES", "s3,sqs")
            .WithEnvironment("DEBUG", "1")
            .WithEnvironment("AWS_DEFAULT_REGION", AuthenticationRegion)
            .WithEnvironment("AWS_ACCESS_KEY_ID", AwsAccessKeyId)
            .WithEnvironment("AWS_SECRET_ACCESS_KEY", AwsSecretAccessKey)
            .WithNetwork(TestContainerConstants.NetworkName)
            .WithNetworkAliases(TestContainerConstants.NetworkAlias)
            .Build();

        await LocalStackContainer.StartAsync();

        InitialiseClients();
        await InitialiseResourcesAsync();
        await VerifyResourcesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        Exception? error = null;

        async ValueTask Safe(Func<ValueTask> f)
        {
            try { await f(); }
            catch (Exception ex) { error ??= ex; }
        }

        // Synchronous disposals wrapped safely
        try { S3Client?.Dispose(); }
        catch (Exception ex) { error ??= ex; }

        try { SqsClient?.Dispose(); }
        catch (Exception ex) { error ??= ex; }

        // Async disposal using the same Safe pattern
        await Safe(() => LocalStackContainer!.DisposeAsync());

        GC.SuppressFinalize(this);

        if (error is not null)
            throw error;
    }

    private void InitialiseClients()
    {
        // S3
        S3Client = new AmazonS3Client(AwsAccessKeyId, AwsSecretAccessKey, new AmazonS3Config
        {
            ServiceURL = ServiceUrl,
            ForcePathStyle = true
        });

        // SQS
        SqsClient = new AmazonSQSClient(GetBasicAWSCredentials, new AmazonSQSConfig
        {
            ServiceURL = ServiceUrl,
            AuthenticationRegion = AuthenticationRegion,
            UseHttp = true
        });

        SqsEndpoint = SqsClient.Config.ServiceURL!;
    }

    private async Task InitialiseResourcesAsync()
    {
        await S3Client.PutBucketAsync(new PutBucketRequest { BucketName = CadsInternalBucketName });
        await S3Client.PutBucketAsync(new PutBucketRequest { BucketName = CadsExternalBucketName });

        var intakeDlqCreated = await SqsClient.CreateQueueAsync(new CreateQueueRequest { QueueName = TestSqsConstants.CadsDeadLetterQueueName });
        var intakeDlqAttr = await SqsClient.GetQueueAttributesAsync(new GetQueueAttributesRequest
        {
            QueueUrl = intakeDlqCreated.QueueUrl,
            AttributeNames = ["QueueArn"]
        });

        var intakeQueueCreated = await SqsClient.CreateQueueAsync(new CreateQueueRequest { QueueName = TestSqsConstants.CadsQueueName });

        if (CadsDeadLetterQueueUrl != intakeDlqCreated.QueueUrl || CadsQueueUrl != intakeQueueCreated.QueueUrl)
        {
            throw new ApplicationException("Localstack queues have unexpected urls");
        }

        var redrivePolicy = $"{{\"deadLetterTargetArn\":\"{intakeDlqAttr.QueueARN}\",\"maxReceiveCount\":\"3\"}}";
        await SqsClient.SetQueueAttributesAsync(new SetQueueAttributesRequest
        {
            QueueUrl = CadsQueueUrl,
            Attributes = new Dictionary<string, string>
            {
                { "RedrivePolicy", redrivePolicy }
            }
        });
    }

    private async Task VerifyResourcesAsync()
    {
        await S3Client.ListObjectsV2Async(new ListObjectsV2Request
        {
            BucketName = CadsInternalBucketName
        });

        await SqsClient.GetQueueAttributesAsync(TestSqsConstants.CadsDeadLetterQueueName, ["All"], CancellationToken.None);
        await SqsClient.GetQueueAttributesAsync(TestSqsConstants.CadsQueueName, ["All"], CancellationToken.None);
    }
}