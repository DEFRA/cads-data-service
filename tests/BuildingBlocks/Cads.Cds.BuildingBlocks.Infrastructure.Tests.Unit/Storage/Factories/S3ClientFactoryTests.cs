using Amazon.S3;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Factories;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Storage.Factories;

public class S3ClientFactoryTests
{
    private readonly S3ClientFactory _clientFactory;
    private readonly AmazonS3Config _defaultAmazonS3Config = new() { RegionEndpoint = Amazon.RegionEndpoint.EUWest2 };

    private const string DefaultBucketName = "test-bucket";

    public S3ClientFactoryTests()
    {
        _clientFactory = new S3ClientFactory();
    }

    [Fact]
    public void GivenClientIsNotRegistered_WhenCallingGetClient_ShouldThrow()
    {
        Action act = () => _clientFactory.GetClient<TestStorageClientA>();

        act.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void GivenClientIsRegistered_WhenCallingGetClient_ShouldReturnRegisteredClient()
    {
        _clientFactory.AddClient<TestStorageClientA>(DefaultBucketName, _defaultAmazonS3Config);

        var result = _clientFactory.GetClient<TestStorageClientA>();

        result.Should().NotBeNull().And.BeAssignableTo<IAmazonS3>();
    }

    [Fact]
    public void GivenClientIsNotRegistered_WhenCallingGetClientByName_ShouldThrow()
    {
        Action act = () => _clientFactory.GetClient(typeof(TestStorageClientA).Name);

        act.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void GivenClientIsRegistered_WhenCallingGetClientByName_ShouldReturnRegisteredClient()
    {
        _clientFactory.AddClient<TestStorageClientA>(DefaultBucketName, _defaultAmazonS3Config);

        var result = _clientFactory.GetClient(typeof(TestStorageClientA).Name);

        result.Should().NotBeNull().And.BeAssignableTo<IAmazonS3>();
    }

    [Fact]
    public void GivenClientIsNotRegistered_WhenCallingGetClientBucketName_ShouldThrow()
    {
        Action act = () => _clientFactory.GetClientBucketName<TestStorageClientA>();

        act.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void GivenClientIsRegistered_WhenCallingGetClientBucketName_ShouldReturnRegisteredClient()
    {
        _clientFactory.AddClient<TestStorageClientA>(DefaultBucketName, _defaultAmazonS3Config);

        var result = _clientFactory.GetClientBucketName<TestStorageClientA>();

        result.Should().NotBeNull().And.Be(DefaultBucketName);
    }

    [Fact]
    public void GivenClientIsNotRegistered_WhenCallingGetClientBucketNameByName_ShouldThrow()
    {
        Action act = () => _clientFactory.GetClientBucketName(typeof(TestStorageClientA).Name);

        act.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void GivenClientIsRegistered_WhenCallingGetClientBucketNameByName_ShouldReturnRegisteredClient()
    {
        _clientFactory.AddClient<TestStorageClientA>(DefaultBucketName, _defaultAmazonS3Config);

        var result = _clientFactory.GetClientBucketName(typeof(TestStorageClientA).Name);

        result.Should().NotBeNull().And.Be(DefaultBucketName);
    }

    [Fact]
    public void GivenNoClientsAreRegistered_WhenCallingGetRegisteredClientNames_ShouldReturnEmptyList()
    {
        var result = _clientFactory.GetRegisteredClientNames();

        result.Should().NotBeNull();
        result.Count().Should().Be(0);
    }

    [Fact]
    public void GivenClientsAreRegistered_WhenCallingGetRegisteredClientNames_ShouldReturnPopulatedList()
    {
        _clientFactory.AddClient<TestStorageClientA>(DefaultBucketName, _defaultAmazonS3Config);
        _clientFactory.AddClient<TestStorageClientB>(DefaultBucketName, _defaultAmazonS3Config);

        var result = _clientFactory.GetRegisteredClientNames();

        result.Should().NotBeNull();
        result.Count().Should().Be(2);
    }

    [Theory]
    [InlineData("TestStorageClientA", true)]
    [InlineData("TestStorageClientB", false)]
    public void GivenInitialisedClients_WhenCallingHasStorageClient_ShouldReturnExpected(string testClientName, bool expectedResult)
    {
        _clientFactory.AddClient<TestStorageClientA>(DefaultBucketName, _defaultAmazonS3Config);

        var result = _clientFactory.HasStorageClient(testClientName);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void WhenCallingAddClient_ShouldRegisterClient()
    {
        _clientFactory.AddClient<TestStorageClientA>(DefaultBucketName, _defaultAmazonS3Config);

        _clientFactory.HasStorageClient("TestStorageClientA").Should().Be(true);
        _clientFactory.GetRegisteredClientNames().Count().Should().Be(1);
        _clientFactory.GetRegisteredClientNames().Should().Contain("TestStorageClientA");
    }

    [Fact]
    public void GivenClientAlreadyRegistered_WhenCallingAddClient_ShouldNotDuplicate()
    {
        _clientFactory.AddClient<TestStorageClientA>(DefaultBucketName, _defaultAmazonS3Config);
        _clientFactory.AddClient<TestStorageClientA>(DefaultBucketName, _defaultAmazonS3Config);

        _clientFactory.HasStorageClient("TestStorageClientA").Should().Be(true);
        _clientFactory.GetRegisteredClientNames().Count().Should().Be(1);
        _clientFactory.GetRegisteredClientNames().Should().Contain("TestStorageClientA");
    }

    [Fact]
    public void WhenCallingAddClientWithCredentials_ShouldRegisterClient()
    {
        Environment.SetEnvironmentVariable("TEST_ACCESS_KEY", "access");
        Environment.SetEnvironmentVariable("TEST_SECRET_KEY", "secret");

        _clientFactory.AddClientWithCredentials<TestStorageClientA>(DefaultBucketName, "TEST_ACCESS_KEY", "TEST_SECRET_KEY", _defaultAmazonS3Config);

        _clientFactory.HasStorageClient("TestStorageClientA").Should().Be(true);
        _clientFactory.GetRegisteredClientNames().Count().Should().Be(1);
        _clientFactory.GetRegisteredClientNames().Should().Contain("TestStorageClientA");
    }

    [Fact]
    public void GivenClientAlreadyRegistered_WhenCallingAddClientWithCredentials_ShouldNotDuplicate()
    {
        Environment.SetEnvironmentVariable("TEST_ACCESS_KEY", "access");
        Environment.SetEnvironmentVariable("TEST_SECRET_KEY", "secret");

        _clientFactory.AddClientWithCredentials<TestStorageClientA>(DefaultBucketName, "TEST_ACCESS_KEY", "TEST_SECRET_KEY", _defaultAmazonS3Config);
        _clientFactory.AddClientWithCredentials<TestStorageClientA>(DefaultBucketName, "TEST_ACCESS_KEY", "TEST_SECRET_KEY", _defaultAmazonS3Config);

        _clientFactory.HasStorageClient("TestStorageClientA").Should().Be(true);
        _clientFactory.GetRegisteredClientNames().Count().Should().Be(1);
        _clientFactory.GetRegisteredClientNames().Should().Contain("TestStorageClientA");
    }

    [Fact]
    public void GivenCredentialsAreNotSet_WhenCallingAddClientWithCredentials_ShouldThrow()
    {
        Environment.SetEnvironmentVariable("TEST_ACCESS_KEY", null);
        Environment.SetEnvironmentVariable("TEST_SECRET_KEY", null);

        Action act = () => _clientFactory.AddClientWithCredentials<TestStorageClientA>(DefaultBucketName, "TEST_ACCESS_KEY", "TEST_SECRET_KEY", _defaultAmazonS3Config);

        act.Should().Throw<InvalidOperationException>();

        _clientFactory.GetRegisteredClientNames().Should().BeEmpty();
    }

    public class TestStorageClientA : IStorageClient
    {
        public string ClientName => "TestStorageClientA";
    }

    public class TestStorageClientB : IStorageClient
    {
        public string ClientName => "TestStorageClientB";
    }
}