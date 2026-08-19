using Amazon.S3;
using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Storage.Models;
using Cads.Cds.BuildingBlocks.Testing.Support.Constants;
using Cads.Cds.StorageBridge.Endpoints.Responses;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Crypto;
using Cads.Cds.StorageBridge.Testing.Support.Constants;
using Cads.Cds.StorageBridge.Tests.Component.TestFixtures;
using FluentAssertions;
using Moq;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Cads.Cds.StorageBridge.Tests.Component.StorageManagement;

public class StorageManagementQueryEndpointTests : IClassFixture<StorageManagementTestFixture>
{
    private const string Endpoint = TestEndpointConstants.StorageBridgeStorageManagementRoot;

    private readonly StorageManagementTestFixture _testFixture;

    public StorageManagementQueryEndpointTests(StorageManagementTestFixture testFixture)
    {
        _testFixture = testFixture;
        _testFixture.Factory.ResetMocks();
    }

    [Fact]
    public async Task WhenBucketsListed_ShouldReturnInternalAndExternalBuckets()
    {
        var response = await _testFixture.HttpClient.GetAsync(
            $"{Endpoint}/buckets",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var buckets = await response.Content.ReadFromJsonAsync<List<StorageBucketResponse>>(
            TestContext.Current.CancellationToken);

        buckets.Should().BeEquivalentTo(
        [
            new StorageBucketResponse("CadsInternalClient", TestS3Constants.TestCadsInternalBucketName),
            new StorageBucketResponse("CadsExternalClient", TestS3Constants.TestCadsExternalBucketName)
        ]);
    }

    [Fact]
    public async Task GivenInternalClient_WhenObjectsListed_ShouldReturnSortedFoldersAndObjects()
    {
        SetupListing(
            folders: ["data/reports/", "data/archive/"],
            keys: ["data/two.csv", "data/one.csv"]);

        var response = await _testFixture.HttpClient.GetAsync(
            $"{Endpoint}/buckets/CadsInternalClient/objects?prefix=data/&delimiter=/&maxKeys=500",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var listing = await response.Content.ReadFromJsonAsync<StorageObjectListing>(
            TestContext.Current.CancellationToken);

        listing.Should().NotBeNull();
        listing!.Folders.Should().ContainInOrder("data/archive/", "data/reports/");
        listing.Objects.Select(o => o.Key).Should().ContainInOrder("data/one.csv", "data/two.csv");
        listing.Objects.Should().AllSatisfy(o => o.Size.Should().Be(42));
        listing.IsTruncated.Should().BeFalse();

        _testFixture.Factory.AmazonS3Mock.Verify(x => x.ListObjectsV2Async(
            It.Is<ListObjectsV2Request>(r =>
                r.BucketName == TestS3Constants.TestCadsInternalBucketName &&
                r.Prefix == "data/" &&
                r.Delimiter == "/" &&
                r.MaxKeys == 500),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GivenPattern_WhenObjectsListed_ShouldFilterFoldersAndObjectsByRelativeName()
    {
        SetupListing(
            folders: ["data/reports/", "data/archive/"],
            keys: ["data/report-jan.csv", "data/other.csv"]);

        var response = await _testFixture.HttpClient.GetAsync(
            $"{Endpoint}/buckets/CadsInternalClient/objects?prefix=data/&delimiter=/&pattern=report",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var listing = await response.Content.ReadFromJsonAsync<StorageObjectListing>(
            TestContext.Current.CancellationToken);

        listing.Should().NotBeNull();
        listing!.Folders.Should().Equal("data/reports/");
        listing.Objects.Select(o => o.Key).Should().Equal("data/report-jan.csv");
    }

    [Fact]
    public async Task GivenInvalidRegexPattern_WhenObjectsListed_ShouldReturnBadRequest()
    {
        var response = await _testFixture.HttpClient.GetAsync(
            $"{Endpoint}/buckets/CadsInternalClient/objects?pattern={Uri.EscapeDataString("[")}&patternMode=regex",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenUnknownClient_WhenObjectsListed_ShouldReturnNotFound()
    {
        var response = await _testFixture.HttpClient.GetAsync(
            $"{Endpoint}/buckets/NoSuchClient/objects",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenPattern_WhenKeysSearched_ShouldReturnMatchingKeysCaseInsensitively()
    {
        SetupListing(
            folders: [],
            keys: ["data/Report-Jan.csv", "data/report-feb.csv", "data/other.csv"]);

        var response = await _testFixture.HttpClient.GetAsync(
            $"{Endpoint}/buckets/CadsInternalClient/search?pattern=report&prefix=data/",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<StorageSearchResponse>(
            TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result!.Keys.Should().BeEquivalentTo("data/Report-Jan.csv", "data/report-feb.csv");

        _testFixture.Factory.AmazonS3Mock.Verify(x => x.ListObjectsV2Async(
            It.Is<ListObjectsV2Request>(r =>
                r.BucketName == TestS3Constants.TestCadsInternalBucketName &&
                r.Prefix == "data/"),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GivenUnknownClient_WhenKeysSearched_ShouldReturnNotFound()
    {
        var response = await _testFixture.HttpClient.GetAsync(
            $"{Endpoint}/buckets/NoSuchClient/search?pattern=report",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenInternalClient_WhenObjectFetched_ShouldStreamBodyAndContentType()
    {
        const string body = "id,name\n1,Cow";

        SetupGetObject(body, contentType: "text/plain");

        var response = await _testFixture.HttpClient.GetAsync(
            $"{Endpoint}/buckets/CadsInternalClient/object?key=data/one.csv",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Headers.ContentType!.MediaType.Should().Be("text/plain");

        var content = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        content.Should().Be(body);

        _testFixture.Factory.AmazonS3Mock.Verify(x => x.GetObjectAsync(
            It.Is<GetObjectRequest>(r =>
                r.BucketName == TestS3Constants.TestCadsInternalBucketName &&
                r.Key == "data/one.csv"),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GivenMissingObject_WhenObjectFetched_ShouldReturnNotFound()
    {
        _testFixture.Factory.AmazonS3Mock
            .Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AmazonS3Exception("no such key") { StatusCode = HttpStatusCode.NotFound });

        var response = await _testFixture.HttpClient.GetAsync(
            $"{Endpoint}/buckets/CadsInternalClient/object?key=data/missing.csv",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenExternalCtsmFile_WhenObjectFetched_ShouldReturnDecryptedCsv()
    {
        const string fileName = "CTSM_APP_TEST_FULL_BATCH1_1_ct_locations_2025-01-01-120000.csv";
        const string plainCsv = "id,name\n1,Cow\n2,Sheep";

        var password = CtsmFilenameParser.Parse(fileName)!.DerivePassword();
        var encrypted = new MemoryStream();
        await new AesCryptoTransform().EncryptStreamAsync(
            new MemoryStream(Encoding.UTF8.GetBytes(plainCsv)),
            encrypted,
            password,
            StorageManagementTestFixture.Salt,
            cancellationToken: TestContext.Current.CancellationToken);
        encrypted.Position = 0;

        _testFixture.Factory.AmazonS3Mock
            .Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetObjectResponse { ResponseStream = encrypted });

        var response = await _testFixture.HttpClient.GetAsync(
            $"{Endpoint}/buckets/CadsExternalClient/object?key=inbound/{fileName}",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Headers.ContentType!.MediaType.Should().Be("text/csv");

        var content = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        content.Should().Be(plainCsv);

        _testFixture.Factory.AmazonS3Mock.Verify(x => x.GetObjectAsync(
            It.Is<GetObjectRequest>(r => r.BucketName == TestS3Constants.TestCadsExternalBucketName),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GivenInternalCtsmFile_WhenObjectFetched_ShouldNotDecrypt()
    {
        const string fileName = "CTSM_APP_TEST_FULL_BATCH1_1_ct_locations_2025-01-01-120000.csv";
        const string plainCsv = "id,name\n1,Cow";

        SetupGetObject(plainCsv, contentType: "text/csv");

        var response = await _testFixture.HttpClient.GetAsync(
            $"{Endpoint}/buckets/CadsInternalClient/object?key=inbound/{fileName}",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        content.Should().Be(plainCsv);
    }

    [Fact]
    public async Task GivenUnknownClient_WhenObjectFetched_ShouldReturnNotFound()
    {
        var response = await _testFixture.HttpClient.GetAsync(
            $"{Endpoint}/buckets/NoSuchClient/object?key=data/one.csv",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private void SetupListing(string[] folders, string[] keys)
    {
        _testFixture.Factory.AmazonS3Mock
            .Setup(x => x.ListObjectsV2Async(It.IsAny<ListObjectsV2Request>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ListObjectsV2Response
            {
                CommonPrefixes = [.. folders],
                S3Objects = [.. keys.Select(key => new S3Object
                {
                    Key = key,
                    Size = 42,
                    LastModified = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    StorageClass = S3StorageClass.Standard
                })],
                IsTruncated = false
            });
    }

    private void SetupGetObject(string body, string contentType)
    {
        var response = new GetObjectResponse
        {
            ResponseStream = new MemoryStream(Encoding.UTF8.GetBytes(body))
        };
        response.Headers.ContentType = contentType;

        _testFixture.Factory.AmazonS3Mock
            .Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
    }
}