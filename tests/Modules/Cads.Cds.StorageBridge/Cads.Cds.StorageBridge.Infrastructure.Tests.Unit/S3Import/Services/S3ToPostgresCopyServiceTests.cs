using Amazon.S3.Model;
using Cads.Cds.BuildingBlocks.Infrastructure.Database;
using Cads.Cds.BuildingBlocks.Testing.Support.Fakes.Streams;
using Cads.Cds.BuildingBlocks.Testing.Support.Utilities.Methods;
using Cads.Cds.StorageBridge.Application.S3Import.Services;
using Cads.Cds.StorageBridge.Core.Domain.Enums;
using Cads.Cds.StorageBridge.Core.DTOs;
using Cads.Cds.StorageBridge.Infrastructure.Persistance.Contexts;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Factories;
using Cads.Cds.StorageBridge.Infrastructure.S3Import.Services;
using Cads.Cds.StorageBridge.Infrastructure.Storage.Clients;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Npgsql;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace Cads.Cds.StorageBridge.Infrastructure.Tests.Unit.S3Import.Services;

public class S3ToPostgresCopyServiceTests
{
    private readonly Mock<IServiceScopeFactory> _scopeFactory = new();
    private readonly Mock<IServiceScope> _scope = new();
    private readonly Mock<IServiceProvider> _provider = new();
    private readonly Mock<IStorageService<CadsInternalClient>> _storageService = new();
    private readonly Mock<IS3ImportCommandFactoryProvider> _factoryProvider = new();
    private readonly Mock<IS3ImportCommandFactory> _factory = new();
    private readonly Mock<ILogger<S3ToPostgresCopyService>> _logger = new();

    private const string TestFileName = "LOCATIONS.part-0001.csv";

    [Fact]
    public async Task ExecuteAsync_ShouldThrow_WhenImportTypeIsNone()
    {
        var service = CreateService();

        var job = new CreateS3CsvImportJobDto
        {
            ImportActionType = ImportActionType.None,
            SourceKey = TestFileName
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.ExecuteAsync(job, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrow_WhenSourceKeyMissing()
    {
        var service = CreateService();

        var job = new CreateS3CsvImportJobDto
        {
            ImportActionType = ImportActionType.Bulk,
            SourceKey = ""
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.ExecuteAsync(job, TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnZero_WhenNoKeysFound()
    {
        var service = CreateService();

        _storageService.Setup(x => x.ListKeysAsync(TestFileName, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var job = new CreateS3CsvImportJobDto
        {
            ImportActionType = ImportActionType.Transactional,
            SourceKey = TestFileName,
            ImportDataType = ImportDataType.Locations
        };

        var result = await service.ExecuteAsync(job, TestContext.Current.CancellationToken);

        result.Should().Be(0);
    }

    [Theory]
    [InlineData("A\"B", "A\"\"B")]
    [InlineData("A\u0001B", "A B")]
    [InlineData(null, "")]
    public void SanitiseLine_ShouldSanitiseCorrectly(string? input, string expected)
    {
        var result = InvokeSanitise(input!);
        result.Should().Be(expected);
    }

    [Fact]
    public async Task GetCommandsAsync_ShouldReturnInsertCommand()
    {
        var insertCmd = new Mock<DbCommand>().Object;

        _factory.Setup(x => x.CreateInsertCommandAsync(It.IsAny<ImportDataType>(), It.IsAny<SchemaName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(insertCmd);

        var job = new CreateS3CsvImportJobDto
        {
            ImportActionType = ImportActionType.Bulk,
            ImportDataType = ImportDataType.Locations
        };

        var result = await InvokeGetCommandsAsync(job, _factory.Object);

        result.Should().ContainSingle().Which.Should().Be(insertCmd);
    }

    [Fact]
    public async Task GetCommandsAsync_ShouldReturnUpdateCommand()
    {
        var updateCmd = new Mock<DbCommand>().Object;

        _factory.Setup(x => x.CreateUpdateCommandAsync(It.IsAny<ImportDataType>(), It.IsAny<SchemaName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(updateCmd);

        var job = new CreateS3CsvImportJobDto
        {
            ImportActionType = ImportActionType.Transactional,
            ImportDataType = ImportDataType.Locations
        };

        var result = await InvokeGetCommandsAsync(job, _factory.Object);

        result.Should().ContainSingle().Which.Should().Be(updateCmd);
    }

    [Fact]
    public async Task GetCommandsAsync_ShouldReturnUpsertCommand_WhenInsertAndUpdate()
    {
        var upsertCmd = new Mock<DbCommand>().Object;

        _factory.Setup(x => x.CreateUpsertCommandAsync(It.IsAny<ImportDataType>(), It.IsAny<SchemaName>(), It.IsAny<  CancellationToken>()))
            .ReturnsAsync(upsertCmd);

        var job = new CreateS3CsvImportJobDto
        {
            ImportActionType = ImportActionType.Bulk,
            ImportDataType = ImportDataType.Locations
        };

        var result = await InvokeGetCommandsAsync(job, _factory.Object);

        result.Should().ContainSingle().Which.Should().Be(upsertCmd);
    }

    [Fact]
    public async Task GetCommandsAsync_ShouldReturnDeleteCommand()
    {
        var deleteCmd = new Mock<DbCommand>().Object;

        _factory.Setup(x => x.CreateDeleteCommandAsync(It.IsAny<ImportDataType>(), It.IsAny<SchemaName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(deleteCmd);

        var job = new CreateS3CsvImportJobDto
        {
            ImportActionType = ImportActionType.Transactional,
            ImportDataType = ImportDataType.Locations
        };

        var result = await InvokeGetCommandsAsync(job, _factory.Object);

        result.Should().ContainSingle().Which.Should().Be(deleteCmd);
    }

    [Fact]
    public async Task ExecuteActionCommandsAsync_ShouldSumResults()
    {
        var service = CreateService();

        var cmd1 = new Mock<DbCommand>();
        cmd1.Setup(x => x.ExecuteNonQueryAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(5);

        var cmd2 = new Mock<DbCommand>();
        cmd2.Setup(x => x.ExecuteNonQueryAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(7);

        var method = MethodInfoUtility.GetPrivate<S3ToPostgresCopyService>("ExecuteActionCommandsAsync");

        var result = await (Task<int>)method!.Invoke(
            service,
            [new[] { cmd1.Object, cmd2.Object }, CancellationToken.None])!;

        result.Should().Be(12);
    }

    [Fact]
    public async Task GivenNullableResponseStream_CopyFileToStagingAsync_ShouldReturnEmpty()
    {
        var service = CreateService();

        var outputStream = new MemoryStream();
        var writer = new NonDisposingStreamWriter(outputStream);

        _storageService
            .Setup(x => x.GetObjectResponseAsync(TestFileName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetObjectResponse());

        var method = typeof(S3ToPostgresCopyService)
            .GetMethod("CopyFileToStagingAsync", BindingFlags.NonPublic | BindingFlags.Instance);

        var fieldInfo = typeof(S3ToPostgresCopyService).GetField("_storageService",
                BindingFlags.NonPublic | BindingFlags.Instance);

        fieldInfo?.SetValue(service, _storageService.Object);

        await (Task)method!.Invoke(service,
            [
            ImportDataType.Locations,
            '|',
            TestFileName,
            _factory.Object,
            CancellationToken.None
            ])!;

        writer.Flush();

        var output = Encoding.UTF8.GetString(outputStream.ToArray());
        output.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task CopyFileToStagingAsync_ShouldWriteSanitisedLines()
    {
        var service = CreateService();

        var outputStream = new MemoryStream();
        var writer = new NonDisposingStreamWriter(outputStream);

        var response = new GetObjectResponse
        {
            ResponseStream = new MemoryStream(Encoding.UTF8.GetBytes("record_type|col2\nA\"B|C\nT|END"))
        };

        _factory.Setup(x => x.CreateTextImport(It.IsAny<ImportDataType>(), It.IsAny<SchemaName>(), It.IsAny<char>(), It.IsAny<List<string>>()))
            .Returns(writer);

        _storageService
            .Setup(x => x.GetObjectResponseAsync(TestFileName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var method = typeof(S3ToPostgresCopyService)
            .GetMethod("CopyFileToStagingAsync", BindingFlags.NonPublic | BindingFlags.Instance);

        var fieldInfo = typeof(S3ToPostgresCopyService).GetField("_storageService",
                BindingFlags.NonPublic | BindingFlags.Instance);

        fieldInfo?.SetValue(service, _storageService.Object);

        await (Task)method!.Invoke(service,
            [
            ImportDataType.Locations,
            '|',
            TestFileName,
            _factory.Object,
            CancellationToken.None
            ])!;

        writer.Flush();
        var output = Encoding.UTF8.GetString(outputStream.ToArray());
        output = output.Replace("\r\n", "\n");
        output.Should().Be("A\"\"B|C\n");
    }

    private S3ToPostgresCopyService CreateService()
    {
        _factoryProvider
          .Setup(x => x.Create(It.IsAny<NpgsqlConnection>()))
          .Returns(_factory.Object);

        _provider.Setup(x => x.GetService(typeof(StorageBridgeWriteDbContext)))
            .Returns(null!);

        _provider.Setup(x => x.GetService(typeof(IStorageService<CadsInternalClient>)))
           .Returns(_storageService.Object);

        _provider.Setup(x => x.GetService(typeof(IS3ImportCommandFactoryProvider)))
           .Returns(_factoryProvider.Object);

        _scope.Setup(x => x.ServiceProvider).Returns(_provider.Object);

        _scopeFactory.Setup(x => x.CreateScope()).Returns(_scope.Object);

        return new S3ToPostgresCopyService(
            _scopeFactory.Object,
            _logger.Object);
    }

    private static string? InvokeSanitise(string input)
    {
        var method = MethodInfoUtility.GetPrivateStatic<S3ToPostgresCopyService>("SanitiseLine");

        return (string?)method!.Invoke(null, [input]);
    }

    private static async Task<List<DbCommand>> InvokeGetCommandsAsync(CreateS3ImportJobDto job, IS3ImportCommandFactory factory)
    {
        var method = MethodInfoUtility.GetPrivateStatic<S3ToPostgresCopyService>("GetCommandsAsync");

        var task = (Task<List<DbCommand>>)method.Invoke(
            null,
            [job, factory, CancellationToken.None])!;

        return await task;
    }
}