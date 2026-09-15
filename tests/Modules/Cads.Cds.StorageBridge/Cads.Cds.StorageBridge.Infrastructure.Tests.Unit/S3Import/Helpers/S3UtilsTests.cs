using Cads.Cds.BuildingBlocks.Application.Imports.Domain.Enums;
using Cads.Cds.BuildingBlocks.Application.Schema;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Helpers;
using FluentAssertions;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.S3Import.Helpers;

public class S3UtilsTests
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
    public async Task TryParseS3Url_ValidNonUriPathInput_ShouldReturnTrue()
    {
        // HTTP style S3 URL
        var s3Url = "https://s3.region.amazonaws.com/my-bucket/path/to/object.txt";

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

    [Fact]
    public void GetImportParameters_InvalidImportActionType_ShouldThrowInvalidOperationException()
    {
        var fileName = "CTSM_CADS_PROD_XXXX_00001_001_CT_SUSPENSE_WG_ALLOC_RULES_2026-08-22-072826.csv";

        Func<ImportParameters> act = () => S3Utils.GetImportParameters(fileName);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"Invalid ImportActionType 'XXXX' for file '{fileName}'.");
    }

    [Fact]
    public void GetImportParameters_InvalidImportDateType_ShouldThrowInvalidOperationException()
    {
        var fileName = "CTSM_CADS_PROD_BULK_00001_001_CT_INVALID_TABLE_2026-08-22-072826.csv";

        Func<ImportParameters> act = () => S3Utils.GetImportParameters(fileName);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"Failed to extract destination table from filename: '{fileName}'.");
    }

    [Fact]
    public void GetImportParameters_ValidFile_ShouldReturnImportParameters()
    {
        var fileName = "CTSM_CADS_PROD_BULK_00001_001_CT_SUSPENSE_WG_ALLOC_RULES_2026-08-22-072826.csv";

        var importParameters = S3Utils.GetImportParameters(fileName);

        importParameters.Should().NotBeNull();
        importParameters.ImportActionType.Should().Be(ImportActionType.Bulk);
        importParameters.SchemaName.Should().Be(SchemaName.CtsTransactions);
        importParameters.ImportDataType.Should().Be(ImportDataType.CtSuspenseWgAllocRules);
    }
}