using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Services;
using FluentAssertions;
using Moq;
using System.Net;
using System.Security.Cryptography;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Storage.Services;

public class S3FileChecksumServiceTests
{
    private const string BucketName = "test-bucket";
    private const string ObjectKey = "data/imports/file.csv";

    private readonly Mock<IS3ClientFactory> _factoryMock = new();
    private readonly Mock<IAmazonS3> _s3Mock = new();

    public S3FileChecksumServiceTests()
    {
        _factoryMock.Setup(f => f.GetClient<TestS3Client>()).Returns(_s3Mock.Object);
        _factoryMock.Setup(f => f.GetClientBucketName<TestS3Client>()).Returns(BucketName);
    }

    [Fact]
    public async Task GivenValidS3Object_WhenComputingChecksum_ShouldReturnExpectedSha256()
    {
        var content = "Hello, World!"u8.ToArray();
        var expected = Convert.ToHexStringLower(SHA256.HashData(content));
        SetupS3Response(content);

        var sut = new S3FileChecksumService<TestS3Client>(_factoryMock.Object);

        var result = await sut.ComputeChecksumAsync(ObjectKey, CancellationToken.None);

        result.Should().Be(expected);
    }

    [Fact]
    public async Task GivenFileContentsChange_WhenComputingChecksum_ShouldReturnDifferentHash()
    {
        var sut = new S3FileChecksumService<TestS3Client>(_factoryMock.Object);

        SetupS3Response("Version one"u8.ToArray());
        var hashA = await sut.ComputeChecksumAsync(ObjectKey, CancellationToken.None);

        SetupS3Response("Version two"u8.ToArray());
        var hashB = await sut.ComputeChecksumAsync(ObjectKey, CancellationToken.None);

        hashA.Should().NotBe(hashB);
    }

    [Fact]
    public async Task GivenSameFileContents_WhenComputingChecksumTwice_ShouldReturnSameHash()
    {
        var content = "Stable content"u8.ToArray();
        var sut = new S3FileChecksumService<TestS3Client>(_factoryMock.Object);

        SetupS3Response(content);
        var hashA = await sut.ComputeChecksumAsync(ObjectKey, CancellationToken.None);

        SetupS3Response(content);
        var hashB = await sut.ComputeChecksumAsync(ObjectKey, CancellationToken.None);

        hashA.Should().Be(hashB);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task GivenEmptyOrWhitespaceLocation_WhenComputingChecksum_ShouldThrowArgumentException(string location)
    {
        var sut = new S3FileChecksumService<TestS3Client>(_factoryMock.Object);

        Func<Task> act = () => sut.ComputeChecksumAsync(location, CancellationToken.None);

        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task GivenValidLocation_WhenComputingChecksum_ShouldRequestCorrectBucketAndKey()
    {
        SetupS3Response("Any"u8.ToArray());
        var sut = new S3FileChecksumService<TestS3Client>(_factoryMock.Object);

        await sut.ComputeChecksumAsync(ObjectKey, CancellationToken.None);

        _s3Mock.Verify(s => s.GetObjectAsync(
            It.Is<GetObjectRequest>(r => r.BucketName == BucketName && r.Key == ObjectKey),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GivenHashReturned_WhenInspectingFormat_ShouldBeLowercaseHex()
    {
        SetupS3Response("Format check"u8.ToArray());
        var sut = new S3FileChecksumService<TestS3Client>(_factoryMock.Object);

        var result = await sut.ComputeChecksumAsync(ObjectKey, CancellationToken.None);

        result.Should().MatchRegex("^[0-9a-f]{64}$", "SHA-256 produces 32 bytes as 64 lowercase hex characters");
    }

    private void SetupS3Response(byte[] content)
    {
        var response = new GetObjectResponse
        {
            ResponseStream = new MemoryStream(content),
            HttpStatusCode = HttpStatusCode.OK
        };

        _s3Mock
            .Setup(s => s.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
    }

    private sealed class TestS3Client : IStorageClient
    {
        public string ClientName => nameof(TestS3Client);
    }
}