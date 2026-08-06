using Cads.Cds.BuildingBlocks.Core.Correlation;
using FluentAssertions;

namespace Cads.Cds.BuildingBlocks.Core.Tests.Unit.Correlation;

public class DeduplicationKeyGeneratorTests
{
    [Fact]
    public void GenerateDeduplicationId_ShouldBeDeterministic()
    {
        var id1 = DeduplicationKeyGenerator.GenerateDeduplicationId(
            bucket: "example-bucket",
            objectKey: "path/abc/import-file.dat",
            fileImportId: "123",
            environment: "PreProd");

        var id2 = DeduplicationKeyGenerator.GenerateDeduplicationId(
            bucket: "example-bucket",
            objectKey: "path/abc/import-file.dat",
            fileImportId: "123",
            environment: "PreProd");

        id1.Should().Be(id2);
    }

    [Fact]
    public void GenerateDeduplicationId_ShouldChangeWhenAnyInputChanges()
    {
        var baseId = DeduplicationKeyGenerator.GenerateDeduplicationId(
            bucket: "example-bucket",
            objectKey: "path/abc/import-file.dat",
            fileImportId: "123",
            environment: "PreProd");

        var changedBucket = DeduplicationKeyGenerator.GenerateDeduplicationId(
            bucket: "bucketB",
            objectKey: "path/abc/import-file.dat",
            fileImportId: "123",
            environment: "PreProd");

        var changedObjectKey = DeduplicationKeyGenerator.GenerateDeduplicationId(
            bucket: "example-bucket",
            objectKey: "other-path/abc/import-file.dat",
            fileImportId: "123",
            environment: "PreProd");

        var changedEtag = DeduplicationKeyGenerator.GenerateDeduplicationId(
            bucket: "example-bucket",
            objectKey: "path/abc/import-file.dat",
            fileImportId: "etag999",
            environment: "PreProd");

        var changedEnvironment = DeduplicationKeyGenerator.GenerateDeduplicationId(
            bucket: "example-bucket",
            objectKey: "path/abc/import-file.dat",
            fileImportId: "123",
            environment: "Prod");

        changedBucket.Should().NotBe(baseId);
        changedObjectKey.Should().NotBe(baseId);
        changedEtag.Should().NotBe(baseId);
        changedEnvironment.Should().NotBe(baseId);
    }

    [Fact]
    public void GenerateDeduplicationId_ShouldReturnValidHexString()
    {
        var id = DeduplicationKeyGenerator.GenerateDeduplicationId(
            bucket: "example-bucket",
            objectKey: "path/abc/import-file.dat",
            fileImportId: "123",
            environment: "PreProd");

        id.Should().NotBeNullOrWhiteSpace();
        id.Length.Should().Be(64); // SHA-256 hex string length
        id.Should().MatchRegex("^[A-F0-9]{64}$");
    }

    [Fact]
    public void GenerateMessageGroupId_ShouldFollowExpectedPattern()
    {
        var groupId = DeduplicationKeyGenerator.GenerateMessageGroupId("path/abc/import-file.dat", "PreProd");

        groupId.Should().Be("path/abc:PreProd");
    }

    [Fact]
    public void GenerateMessageGroupId_ShouldChangeWhenInputsChange()
    {
        var id1 = DeduplicationKeyGenerator.GenerateMessageGroupId("path/abc/import-file.dat", "PreProd");
        var id2 = DeduplicationKeyGenerator.GenerateMessageGroupId("path/abc/import-file.dat", "Prod");
        var id3 = DeduplicationKeyGenerator.GenerateMessageGroupId("path/def/import-file.dat", "PreProd");

        id2.Should().NotBe(id1);
        id3.Should().NotBe(id1);
    }

    [Fact]
    public void GenerateMessageGroupId_ShouldUseWholeObjectKey_WhenNoSlashPresent()
    {
        var groupId = DeduplicationKeyGenerator.GenerateMessageGroupId("import-file.dat", "PreProd");

        groupId.Should().Be("import-file.dat:PreProd");
    }
}