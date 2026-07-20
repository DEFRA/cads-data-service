using Cads.Cds.BuildingBlocks.Application.Files.Abstractions;
using Cads.Cds.BuildingBlocks.Infrastructure.Files.Services;
using Cads.Cds.BuildingBlocks.Infrastructure.Json;
using FluentAssertions;
using Moq;
using System.Text.Json;

namespace Cads.Cds.BuildingBlocks.Infrastructure.Tests.Unit.Files.Services;

public class FileServiceTests
{
    private readonly Mock<IFileSystem> _fileSystem = new();
    private readonly FileService _sut;

    private const string TestFilePath = "\\test\\path\test.json";

    public FileServiceTests()
    {
        _sut = new FileService(_fileSystem.Object);
    }

    [Fact]
    public async Task WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        _fileSystem.Setup(fs => fs.Exists(TestFilePath)).Returns(false);

        Func<Task> act = () => _sut.ReadJsonFromFileAndReturnAsModelAsync<TestModel>(TestFilePath, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<FileNotFoundException>()
            .WithMessage($"File not found: {TestFilePath}");
    }

    [Fact]
    public async Task WhenFileCannotBeRead_ThrowsIOException()
    {
        _fileSystem.Setup(fs => fs.Exists(TestFilePath)).Returns(true);
        _fileSystem.Setup(fs => fs.ReadAllBytesAsync(TestFilePath, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Exception"));

        Func<Task> act = () => _sut.ReadJsonFromFileAndReturnAsModelAsync<TestModel>(TestFilePath, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<IOException>()
            .WithMessage($"Failed to read file '{TestFilePath}'.*");
    }

    [Fact]
    public async Task WhenJsonIsInvalid_ThrowsFileLoadException()
    {
        _fileSystem.Setup(fs => fs.Exists(TestFilePath)).Returns(true);
        _fileSystem.Setup(fs => fs.ReadAllBytesAsync(TestFilePath, It.IsAny<CancellationToken>()))
            .ReturnsAsync([0x01, 0x02, 0x03]);

        Func<Task> act = () => _sut.ReadJsonFromFileAndReturnAsModelAsync<TestModel>(TestFilePath, TestContext.Current.CancellationToken);

        await act.Should().ThrowAsync<FileLoadException>()
            .WithMessage($"Failed to deserialize JSON from '{TestFilePath}'.*");
    }

    [Fact]
    public async Task WhenJsonIsValid_ReturnsDeserializedModel()
    {
        var json = JsonSerializer.Serialize(
            new TestModel { Name = "Test Name", Value = 42 },
            JsonDefaults.DefaultOptionsWithIndented
        );
        var bytes = System.Text.Encoding.UTF8.GetBytes(json);

        _fileSystem.Setup(fs => fs.Exists(TestFilePath)).Returns(true);
        _fileSystem.Setup(fs => fs.ReadAllBytesAsync(TestFilePath, It.IsAny<CancellationToken>()))
            .ReturnsAsync(bytes);

        var result = await _sut.ReadJsonFromFileAndReturnAsModelAsync<TestModel>(TestFilePath, TestContext.Current.CancellationToken);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Test Name");
        result.Value.Should().Be(42);
    }

    public sealed class TestModel
    {
        public string? Name { get; set; }
        public int Value { get; set; }
    }
}