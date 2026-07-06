using Cads.Cds.StorageBridge.Infrastructure.S3Import.Helpers;
using FluentAssertions;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.S3Import.Helpers;

public class GetLocationsQueryValidatorTests
{
    [Fact]
    public async Task TryParseS3Url_ValidUri_ShouldReturnTrue()
    {
        var s3Url = "s3://my-bucket/path/to/object.txt";

        var result = S3Utils.TryParseS3Url(s3Url, out var bucketName, out var objectKey, out var fileName);

        result.Should().BeTrue();
        bucketName.Should().Be("my-bucket");
        objectKey.Should().Be("path/to/object.txt");
        fileName.Should().Be("object.txt");
    }

    [Fact]
    public async Task TryParseS3Url_ValidNonUriInput_ShouldReturnTrue()
    {
        // HTTP style S3 URL
        var s3Url = "https://my-bucket.s3.amazonaws.com/path/to/object.txt";

        var result = S3Utils.TryParseS3Url(s3Url, out var bucketName, out var objectKey, out var fileName);

        result.Should().BeTrue();
        bucketName.Should().Be("my-bucket");
        objectKey.Should().Be("path/to/object.txt");
        fileName.Should().Be("object.txt");
    }

    [Fact]
    public async Task TryParseS3Url_UriWithoutPrefix_ShouldReturnTrue()
    {
        var s3Url = "my-bucket.s3.amazonaws.com/path/to/object.txt";

        var result = S3Utils.TryParseS3Url(s3Url, out var bucketName, out var objectKey, out var fileName);

        result.Should().BeTrue();
        bucketName.Should().Be("");
        objectKey.Should().Be("my-bucket.s3.amazonaws.com/path/to/object.txt");
        fileName.Should().Be("object.txt");
    }

    [Fact]
    public async Task TryParseS3Url_Empty_InvalidUri_ShouldReturnFalse()
    {
        var s3Url = "";

        var result = S3Utils.TryParseS3Url(s3Url, out var bucketName, out var objectKey, out var fileName);

        result.Should().BeFalse();
        bucketName.Should().BeNullOrEmpty();
        objectKey.Should().BeNullOrEmpty();
        fileName.Should().BeNullOrEmpty();
    }
}