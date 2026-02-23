using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using Testcontainers.LocalStack;
using Xunit;

namespace Cads.Cds.BuildingBlocks.Testing.Support.TestFixtures;

// ReSharper disable once ClassNeverInstantiated.Global
public class LocalStackFixture : IAsyncLifetime
{
    public LocalStackContainer? LocalStackContainer { get; private set; }

    public IAmazonSQS SqsClient { get; private set; } = null!;
    public IAmazonS3 S3Client { get; private set; } = null!;
    public const string AwsAccessKeyId = "test";
    public const string AwsSecretAccessKey = "test";
    private static Amazon.Runtime.BasicAWSCredentials GetBasicAWSCredentials => new(AwsAccessKeyId, AwsSecretAccessKey);
    public string? SqsEndpoint { get; private set; }

    public const string AuthenticationRegion = "eu-west-2";
    private const string NetworkName = "integration-test-network";
    private const string CadsQueueName = "cads-cds-queue";
    private const string CadsDeadLetterQueueName = "cads-cds-queue-deadletter";

    public const string NetworkAlias = "localstack";
    public const int Port = 4566;
    public string ServiceUrl => $"http://localhost:{Port}";
    public string NetworkServiceUrl => $"http://{NetworkAlias}:{Port}";
    public const string CadsInternalBucketName = "cads-internal-bucket";
    public string CadsIntakeQueueUrl => $"http://sqs.eu-west-2.localhost.localstack.cloud:{Port}/000000000000/cads-cds-queue";
    public string CadsDeadLetterQueueUrl => $"http://sqs.eu-west-2.localhost.localstack.cloud:{Port}/000000000000/cads-cds-queue-deadletter";

    public async ValueTask InitializeAsync()
    {
        DockerNetworkHelper.EnsureNetworkExists(NetworkName);

        LocalStackContainer = new LocalStackBuilder("localstack/localstack:3.0.2")
            .WithName("localstack")
            .WithEnvironment("SERVICES", "s3,sqs")
            .WithEnvironment("DEBUG", "1")
            .WithEnvironment("AWS_DEFAULT_REGION", AuthenticationRegion)
            .WithEnvironment("AWS_ACCESS_KEY_ID", AwsAccessKeyId)
            .WithEnvironment("AWS_SECRET_ACCESS_KEY", AwsSecretAccessKey)
            .WithEnvironment("EDGE_PORT", $"{Port}")
            .WithPortBinding(Port, Port)
            .WithNetwork(NetworkName)
            .WithNetworkAliases(NetworkAlias)
            .Build();

        await LocalStackContainer.StartAsync();

        InitialiseClients();
        await InitialiseResourcesAsync();
        await VerifyResourcesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            S3Client?.Dispose();
            SqsClient?.Dispose();
        }
        finally
        {
            await LocalStackContainer!.DisposeAsync();
        }
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

        var intakeDlqCreated = await SqsClient.CreateQueueAsync(new CreateQueueRequest { QueueName = CadsDeadLetterQueueName });
        var intakeDlqAttr = await SqsClient.GetQueueAttributesAsync(new GetQueueAttributesRequest
        {
            QueueUrl = intakeDlqCreated.QueueUrl,
            AttributeNames = ["QueueArn"]
        });

        var intakeQueueCreated = await SqsClient.CreateQueueAsync(new CreateQueueRequest { QueueName = CadsQueueName });

        if (CadsDeadLetterQueueUrl != intakeDlqCreated.QueueUrl || CadsIntakeQueueUrl != intakeQueueCreated.QueueUrl)
        {
            throw new ApplicationException("Localstack queues have unexpected urls");
        }

        var redrivePolicy = $"{{\"deadLetterTargetArn\":\"{intakeDlqAttr.QueueARN}\",\"maxReceiveCount\":\"3\"}}";
        await SqsClient.SetQueueAttributesAsync(new SetQueueAttributesRequest
        {
            QueueUrl = CadsIntakeQueueUrl,
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

        await SqsClient.GetQueueAttributesAsync(CadsDeadLetterQueueName, ["All"], CancellationToken.None);
        await SqsClient.GetQueueAttributesAsync(CadsQueueName, ["All"], CancellationToken.None);
    }
}